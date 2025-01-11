using AutoFixture;
using EagleCore;
using EagleServices;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace EagleTests
{
	public class BotLocationTests
	{
		private readonly ServiceProvider serviceProvider;
		private readonly Fixture fixture = new();


		public BotLocationTests()
		{
			var services = new ServiceCollection();
			services.AddMemoryCache();
			services.AddSingleton<IBotStorage, BotStorage>();
			services.AddSingleton<IBotVerification, BotVerification>();
			services.AddSingleton<BotService>();
			serviceProvider = services.BuildServiceProvider();
		}

		/// <summary>
		/// This is a simulated test and should not remain 
		/// This test could cause a flaky tests for others... 
		/// </summary>
		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public void TestHeightSpecifiedShouldCauseAFailure(bool verifyResult)
		{
			var record = fixture.Create<BotRecord>();
			var storageMock = new Mock<IBotStorage>();
			var verifyMock = new Mock<IBotVerification>();
			verifyMock.Setup(x => x.Verify(It.IsAny<BotRecord>())).Returns(verifyResult);

			var botService = new BotService(storageMock.Object, verifyMock.Object);

			//ACT
			var response = botService.Add(record);


			//ASSERT
			response.Success.Should().Be(verifyResult);
		}

		[Fact]
		public void AddShouldAddARecordToTheCache()
		{
			//ARRANGE
			var record = fixture.Create<BotRecord>();
			var storageMock = new Mock<IBotStorage>();
			var verifyMock = new Mock<IBotVerification>();
			verifyMock.Setup(x => x.Verify(It.IsAny<BotRecord>())).Returns(true);

			var botService = new BotService(storageMock.Object, verifyMock.Object);

			//ACT
			botService.Add(record);

			//ASSERT
			storageMock.Verify(x => x.AddLocation(It.IsAny<BotRecord>()), Times.Once);
		}


		[Fact]
		public void ShouldHaveANumberofBotLocationsRecorded()
		{
			//ARRANGE
			var records = fixture.Build<BotRecord>().CreateMany(20);

			var botService = serviceProvider.GetRequiredService<BotService>();
			var storage = serviceProvider.GetRequiredService<IBotStorage>();

			storage.Should().BeOfType<BotStorage>();
			var botStorage = storage as BotStorage;

			foreach (var r in records)
			{
				botService.Add(r);
			}

			//ACT
			var savedRecords = botService.GetAll();


			//ASSERT
			savedRecords.Count.Should().Be(20);
		}

		[Fact]
		public void AddingMultipleLocationsForTheSameBotShouldOnlyStoreTheLatestLocation()
		{
			//ARRANGE
			var records = fixture.Build<BotRecord>().With(f => f.Identifier, fixture.Create<Guid>()).CreateMany(20);

			var botService = serviceProvider.GetRequiredService<BotService>();
			var storage = serviceProvider.GetRequiredService<IBotStorage>();

			storage.Should().BeOfType<BotStorage>();
			var botStorage = storage as BotStorage;

			foreach (var r in records)
			{
				botService.Add(r);
			}

			//ACT
			var savedRecords = botService.GetAll();


			//ASSERT
			savedRecords.Count.Should().Be(1);
		}
	}
}