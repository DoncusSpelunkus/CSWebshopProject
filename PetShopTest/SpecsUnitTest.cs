using Moq;
using PetShop.Application.Interfaces;
using PetShop.Domain;

namespace PetShopTest;

public class   SpecsUnitTest
{
    [Fact]
    public void getAllProducts_validData()
    {
        Mock<ISpecRepo> Specrepo = new Mock<ISpecRepo>();
        List<Specs> Expected = new List<Specs>();
        var specs1 = new Specs()
        {
            ID = 1, SpecName = "HelloWord"
        };
        var specs2 = new Specs()
        {
            ID = 2, SpecName = "HelloWord2"
        };var specs3 = new Specs()
        {
            ID = 3, SpecName = "HelloWord1"
        };
        Expected.Add(specs1);
        Expected.Add(specs2);
        Expected.Add(specs3);

        Specrepo.Setup(repo => repo.GetAllSpecs()).Returns(Expected);

        List<Specs> actual = new List<Specs>();
        
        var actual1 = new Specs()
        {
            ID = 1, SpecName = "HelloWord"
        };
        var actual2 = new Specs()
        {
            ID = 2, SpecName = "HelloWord2"
        };var actual3 = new Specs()
        {
            ID = 3, SpecName = "HelloWord1"
        };
        actual.Add(actual1);
        actual.Add(actual2);
        actual.Add(actual3);

        
        Assert.Equivalent(Expected,actual);

    }

    [Fact]
    public void getAllProducts_InvalidData()
    {
        Mock<ISpecRepo> Specrepo = new Mock<ISpecRepo>();
        List<Specs> Expected = new List<Specs>();
        var specs1 = new Specs()
        {
            ID = 1, SpecName = "HelloWord"
        };
        var specs2 = new Specs()
        {
            ID = 2, SpecName = "HelloWord2"
        };var specs3 = new Specs()
        {
            ID = 3, SpecName = "HelloWord1"
        };
        Expected.Add(specs1);
        Expected.Add(specs2);
        Expected.Add(specs3);

        Specrepo.Setup(repo => repo.GetAllSpecs()).Returns(Expected);

        List<Specs> actual = new List<Specs>();
        
        var actual1 = new Specs()
        {
            ID = 2, SpecName = "HelloWord"
        };
        var actual2 = new Specs()
        {
            ID = 5, SpecName = "Something"
        };var actual3 = new Specs()
        {
            ID = 3, SpecName = "HelloWord1"
        };
        actual.Add(actual1);
        actual.Add(actual2);
        actual.Add(actual3);

        
        Assert.NotEqual(Expected,actual);
    }
    [Fact]
    public void CreateProduct_validData()
    {
        Mock<ISpecRepo> Specrepo = new Mock<ISpecRepo>();
        Specs Expected = new Specs()
        {
            ID = 1, SpecName = "Iphone"
        };

        Specrepo.Setup(repo => repo.CreateSpecs(Expected)).Returns(Expected);

        var actual = new Specs()
        {
            ID = 1, SpecName = "Iphone"
        };
        
        Assert.Equivalent(Expected,actual);
    }

    [Fact]
    public void DeleteByID_ValidData()
    {
        int id = 1;
        Mock<ISpecRepo> specRepo = new Mock<ISpecRepo>();
        Specs expected = new Specs()
        {
            ID = id, SpecName = "Iphone"
        };

        specRepo.Setup(s => s.DeleteSpecsById(id)).Returns(expected);

        ISpecRepo repo = specRepo.Object;

        var actual = repo.DeleteSpecsById(id);
        
        Assert.Equal(expected,actual);
        


    }

    [Fact]
    public void deletById_InvalidData()
    {
        int id = 1;

        Mock<ISpecRepo> specRepo = new Mock<ISpecRepo>();
        Specs expected = new Specs()
        {
            ID = id, SpecName = "iphone"
        };

        specRepo.Setup(s => s.DeleteSpecsById(id)).Returns(expected);

        ISpecRepo service = specRepo.Object;

        var actualSpecs = service.DeleteSpecsById(2);
        
        Assert.NotEqual(expected, actualSpecs);

    }
}