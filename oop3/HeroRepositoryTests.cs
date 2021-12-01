using System;
using NUnit.Framework;

public class HeroRepositoryTests
{
    private HeroRepository heroRepository;
    private Hero hero;

    [SetUp]
    public void SetUp()
    {
        heroRepository = new HeroRepository();
        hero = new Hero("Ivan", 12);
    }

    [Test]
    public void CreateIsNull()
    {
        Hero hero = null;

        Assert.That(() => heroRepository.Create(hero),
            Throws.ArgumentNullException);
    }

    [Test]
    public void TestCreate()
    {
        string message = heroRepository.Create(hero);
        string exspectedMessage = $"Successfully added hero {hero.Name} with level {hero.Level}";

        Assert.AreEqual(message, exspectedMessage);
    }

    [Test]
    public void TestAlreadyExists()
    {
        heroRepository.Create(hero);

        Assert.That(() => heroRepository.Create(hero),
            Throws.InvalidOperationException);
    }

    [Test]
    public void TestRemove()
    {
        heroRepository.Create(hero);
        bool isRemove = heroRepository.Remove(hero.Name);

        Assert.IsTrue(isRemove);
    }

    [Test]
    public void TestRemoveIsNull()
    {
        Assert.That(() => heroRepository.Remove(null),
            Throws.ArgumentNullException);
    }

    [Test]
    public void TestHighestLevelHero()
    {
        Hero heroOne = new Hero("Ivan", 12);
        Hero heroTwo = new Hero("Pesho", 43);

        heroRepository.Create(heroOne);
        heroRepository.Create(heroTwo);

        Hero highHero = heroRepository.GetHeroWithHighestLevel();

        Assert.AreEqual(heroTwo, highHero);
    }

    [Test]
    public void TestGetHero()
    {
        Hero heroOne = new Hero("Ivan", 12);
        Hero heroTwo = new Hero("Pesho", 43);

        heroRepository.Create(heroOne);
        heroRepository.Create(heroTwo);

        Hero getHero = heroRepository.GetHero(heroTwo.Name);

        Assert.AreEqual(heroTwo, getHero);
    }

    [Test]
    public void TestGetCollection()
    {
        Hero heroOne = new Hero("Ivan", 12);
        Hero heroTwo = new Hero("Pesho", 43);

        heroRepository.Create(heroOne);
        heroRepository.Create(heroTwo);

        Assert.That(() => heroRepository.Heroes.Count == 2);
    }
}