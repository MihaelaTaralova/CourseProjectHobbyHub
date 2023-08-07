using HobbyBubSystem.Data.Models;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using HobbyHubSystem.Web.ViewModels.Event;
using Microsoft.EntityFrameworkCore;
using static HobbyHubSystem.Tests.DatabaseSeeder;

namespace HobbyHubSystem.Tests
{
    public class EventServiceTests
    {
        private DbContextOptions<HobbyHubDbContext> options;
        private HobbyHubDbContext dbContext;
        private IEventService eventService;

        [SetUp]
        public void Setup()
        {

            this.options = new DbContextOptionsBuilder<HobbyHubDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            dbContext = new HobbyHubDbContext(options);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.eventService = new EventService(dbContext);

        }

        [TearDown]
        public void CleanUp()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task AddEventAsync_ShouldAddEventToDatabase()
        {
            // Arrange
            var eventViewModel = new AddEventViewModel
            {
                Title = "Test Event",
                Description = "Test Event Description",
                Location = "Test Location",
                DateOfEvent = DateTime.Now,
                HubId = 1
            };

            var creatorId = Guid.NewGuid();

            // Act
            await eventService.AddEventAsync(eventViewModel, creatorId);

            // Assert
            var addedEvent = dbContext.Events.FirstOrDefault(e => e.Title == "Test Event");
            Assert.NotNull(addedEvent);
            Assert.AreEqual("Test Event", addedEvent.Title);
            Assert.AreEqual("Test Event Description", addedEvent.Description);
            Assert.AreEqual("Test Location", addedEvent.Location);
            Assert.AreEqual(creatorId, addedEvent.CreatorId);
            Assert.AreEqual(1, addedEvent.HubId);
        }

        [Test]
        public async Task DeleteEvent_ShouldSetIsActiveToFalse()
        {
            // Arrange
            var currentEvent = dbContext.Events.First();
            var eventId = currentEvent.Id;

            // Act
            await eventService.DeleteEvent(eventId);

            // Assert
            var deletedEvent = dbContext.Events.Find(eventId);
            Assert.NotNull(deletedEvent);
            Assert.IsFalse(deletedEvent.IsActive);
        }

        [Test]
        public async Task DeleteEvent_NonExistentEvent_ShouldThrowArgumentException()
        {
            // Arrange
            var nonExistentEventId = 999;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await eventService.DeleteEvent(nonExistentEventId));
        }

        [Test]
        public async Task EditEvent_ShouldUpdateEventFields()
        {
            // Arrange
            var currentEvent = dbContext.Events.First();
            var eventId = currentEvent.Id;
            var originalEvent = dbContext.Events.FirstOrDefault(e => e.Id == eventId);
            var editModel = new EditEventViewModel
            {
                Title = "Edited Event Title",
                Description = "Edited Event Description",
                Location = "Edited Event Location",
                DateOfEvent = DateTime.UtcNow.AddDays(30)
            };

            // Act
            await eventService.EditEvent(eventId, editModel);

            // Assert
            var editedEvent = dbContext.Events.FirstOrDefault(e => e.Id == eventId);
            Assert.NotNull(editedEvent);
            Assert.AreEqual(editModel.Title, editedEvent.Title);
            Assert.AreEqual(editModel.Description, editedEvent.Description);
            Assert.AreEqual(editModel.Location, editedEvent.Location);
            Assert.AreEqual(editModel.DateOfEvent, editedEvent.DateOfEvent);
        }

        [Test]
        public async Task GetAllEventsAsync_ShouldReturnAllActiveEventsForHub()
        {
            // Arrange
            var hub = dbContext.Hubs.FirstOrDefault();
            if (hub == null)
            {
                Assert.Fail("No hubs found in the database.");
            }
            var hubId = hub.Id;

            var mockEvent1 = new Event()
            {
                Id = 1,
                HubId = hubId,
                IsActive = true,
                Location = "Test",
                Title = "Test1",
                Description = "Test of test 1"
            };

            var mockEvent2 = new Event()
            {
                Id = 2,
                HubId = hubId,
                IsActive = true,
                Location = "TestTest",
                Title = "Test2",
                Description = "Test of test 2"
            };

            var mockEvent3 = new Event()
            {
                Id = 3,
                HubId = hubId,
                IsActive = false,
                Location = "Test",
                Title = "Test3",
                Description = "Test of test 3"
            };

            var mockEvent4 = new Event()
            {
                Id = 4,
                HubId = hubId,
                IsActive = false,
                Location = "Test",
                Title = "Test3",
                Description = "Test of test 3"
            };

            dbContext.Events.AddRange(mockEvent1, mockEvent2, mockEvent3, mockEvent4);
            await dbContext.SaveChangesAsync();

            // Act
            var events = await eventService.GetAllEventsAsync(hubId);

            // Assert
            Assert.NotNull(events);
            Assert.AreEqual(3, events.Count);
        }

        [Test]
        public async Task GetEventByIdAsync_ShouldReturnCorrectEvent()
        {
            // Arrange
            var eventId = 14; 

            // Act
            var eventById = await eventService.GetEventByIdAsync(eventId);

            // Assert
            Assert.NotNull(eventById);
            Assert.AreEqual(eventId, eventById.Id);
        }

        [Test]
        public async Task GetEventByNameAsync_ShouldReturnCorrectEvent()
        {
            // Arrange
            var eventTitle = "Sample Event"; 

            // Act
            var eventByName = await eventService.GetEventByNameAsync(eventTitle);

            // Assert
            Assert.NotNull(eventByName);
            Assert.AreEqual(eventTitle, eventByName.Title);
        }

        [Test]
        public async Task GetEventAsync_ShouldReturnCorrectViewModel()
        {
            // Arrange
            var eventId = 14; 

            // Act
            var eventViewModel = await eventService.GetEventAsync(eventId);

            // Assert
            Assert.NotNull(eventViewModel);
            Assert.AreEqual(eventId, eventViewModel.Id);
            
        }

        [Test]
        public async Task IsUserJoinedEvent_ShouldReturnTrueIfUserJoined()
        {
            // Arrange
            var eventId = 14; 
            var userId = Guid.Parse("D5471536-F4EB-42E0-A5BB-24105BF656D6"); 

            var hobbyUserEvent = new HobbyUserEvent
            {
                EventId = eventId,
                HobbyUserId = userId
            };

            dbContext.HobbyUserEvents.Add(hobbyUserEvent);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await eventService.IsUserJoinedEvent(eventId, userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task JoinEventAsync_ShouldJoinEventForUser()
        {
            // Arrange
            var hub = dbContext.Hubs.FirstOrDefault();
            if (hub == null)
            {
                Assert.Fail("No hubs found in the database.");
            }
            var hubId = hub.Id;

            var mockEvent = new Event()
            {
                Id = 17,
                HubId = hubId,
                IsActive = true,
                Location = "Test",
                Title = "Test1",
                Description = "Test of test 1"
            };

            dbContext.Events.Add(mockEvent);
            await dbContext.SaveChangesAsync();

            var eventId = mockEvent.Id;
            var userId = Guid.Parse("8D48147E-3394-4115-9D14-03B169C4F13F");

            // Act
            await eventService.JoinEventAsync(eventId, userId);

            // Assert
            var isUserJoined = await dbContext.HobbyUserEvents.FirstOrDefaultAsync(e => e.EventId == eventId && e.HobbyUserId == userId);
            Assert.NotNull(isUserJoined);
        }

        [Test]
        public void ConvertEventModelToViewModel_ShouldConvertCorrectly()
        {
            // Arrange
            var eventModel = new Event
            {
                Id = 1,
                Title = "Test Event",
                Description = "Description for test event",
                DateOfEvent = DateTime.UtcNow.AddDays(7),
                Location = "Test Location"
            };

            var eventService = new EventService(dbContext); 
            var eventViewModel = eventService.ConvertEventModelToViewModel(eventModel);

            // Assert
            Assert.NotNull(eventViewModel);
            Assert.AreEqual(eventModel.Id, eventViewModel.Id);
            Assert.AreEqual(eventModel.Title, eventViewModel.Title);
            Assert.AreEqual(eventModel.Description, eventViewModel.Description);
            Assert.AreEqual(eventModel.DateOfEvent, eventViewModel.DateOfEvent);
            Assert.AreEqual(eventModel.Location, eventViewModel.Location);
        }

    }
}
