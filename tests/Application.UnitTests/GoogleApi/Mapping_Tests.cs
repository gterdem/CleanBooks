using System.Runtime.Serialization;
using AutoMapper;
using CleanBooks.Application.Books.Queries.GetBooks;
using CleanBooks.Application.Common.Mappings;
using CleanBooks.Domain.Entities;
using CleanBooks.Domain.Entities.VolumeInfoData;
using FluentAssertions;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using NUnit.Framework;

namespace CleanBooks.Application.UnitTests.GoogleApi;

public class Mapping_Tests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;
    
    public Mapping_Tests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public async Task ShouldMapToVolumeDtoFromGoogleData()
    {
        // https://www.googleapis.com/books/v1/volumes?q=flowers+inauthor:keyes
        var query = "flowers+inauthor:keyes";
        
        BooksService service = new BooksService();
        var listRequest = service.Volumes.List(query);

        var result = await listRequest.ExecuteAsync(new CancellationToken());
        result.Items.Count.Should().Be(10);
        var bookDtos = BookMapper.MapGoogleVolumesDataToVolumeDto(result);

        bookDtos.Books.Count.Should().Be(result.Items.Count);
        bookDtos.Kind.Should().Be(result.Kind);
    }
    
    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }
    
    [Test]
    [TestCase(typeof(Book), typeof(BookDto))]
    [TestCase(typeof(VolumeInfo), typeof(VolumeInfoDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}