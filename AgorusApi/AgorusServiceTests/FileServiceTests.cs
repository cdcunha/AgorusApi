using AgorusApi.Dto;
using AgorusApi.Model;
using AgorusApi.Profiles;
using AgorusService.Repositories.Interfaces;
using AgorusService.Services;
using AgorusService.Services.Interfaces;
using AutoMapper;
using FakeItEasy;
using Newtonsoft.Json;

namespace AgorusServiceTests
{
    public class FileServiceTests
    {
        private readonly IFileService fileService;
        private readonly IFileRepository _fakeFileRepository;
        private readonly IFileHistoryRepository _fakeFileHistoryRepository;

        public FileServiceTests()
        {
            _fakeFileRepository = A.Fake<IFileRepository>();
            
            _fakeFileHistoryRepository = A.Fake<IFileHistoryRepository>();

            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new FileProfile());
            });

            IMapper mapper = new Mapper(mapperConfig);

            fileService = new FileService(mapper, _fakeFileRepository, _fakeFileHistoryRepository);
        }

        public static IEnumerable<object[]> ReadByIdWithDetailsData => new List<object[]>
        {
            new object[] { null, null, null },
            new object[] { -1, null, null },
            new object[] { 0, null, null },
            new object[] { int.MinValue, new FileModel() { FileModelId = int.MinValue, Name = $"FileTeste_{int.MinValue}", ContentType = "Test" }, new FileDto() { FileModelId = int.MinValue, Name = $"FileTeste_{int.MinValue}", ContentType = "Test" } },
            new object[] { int.MaxValue, new FileModel() { FileModelId = int.MaxValue, Name = $"FileTeste_{int.MaxValue}", ContentType = "Test" }, new FileDto() { FileModelId = int.MaxValue, Name = $"FileTeste_{int.MaxValue}", ContentType = "Test" } },
        };

        [Theory]
        [MemberData(nameof(ReadByIdWithDetailsData))]
        public async Task ReadByIdWithDetailsTest(int? id, FileModel? fileModel, FileDto? expected)
        {
            //arrange
            A.CallTo(() => _fakeFileRepository.ReadByIdWithDetail(id)).Returns(fileModel);

            //act
            var result = await fileService.ReadByIdWithDetail(id);

            //assertion
            var expectedSer = JsonConvert.SerializeObject(expected);
            var resultSer = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedSer, resultSer);
        }
    }
}