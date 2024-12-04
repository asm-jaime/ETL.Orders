using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ETL.Orders.Tests;

[TestFixture]
public partial class DatabaseTests
{
    [Test]
    public async Task Test_ProcessXmlFile_InvalidXmlStructure_ShouldThrowExceptionOnInvalidFIO()
    {
        // Arrange
        var xmlFileProcessingService = GetXmlFileProcessingService();
        var testFilePath = @"test_data5_invalid_fio.xml";

        // Act
        Func<Task> act = async () => await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        await act.Should().ThrowAsync<FormatException>();
    }

    [Test]
    public async Task Test_ProcessXmlFile_MissingRequiredTags_User()
    {
        // Arrange
        var xmlFileProcessingService = GetXmlFileProcessingService();
        var testFilePath = @"test_data6_invalid_user.xml";

        // Act
        Func<Task> act = async () => await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        await act.Should().ThrowAsync<FormatException>();
    }
}

