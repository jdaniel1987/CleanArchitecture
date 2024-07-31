using AutoFixture.Xunit2;
using CleanArchitecture.Domain.ValueObjects;
using FluentAssertions;

namespace CleanArchitecture.Domain.Tests.ValueObjects;

public class PriceTests
{
    [Theory, AutoData] // AutoData autogenerates test param values
    public void Should_create_price(double priceInUsd)
    {
        var price = Price.Create(priceInUsd);

        price.Value.Should().Be(priceInUsd);
    }

    [Theory, AutoData] // AutoData autogenerates test param values
    public void Should_convert_to_price_in_euros(Price price)
    {
        PriceEuros priceEuuros = price;
        priceEuuros.Value.Should().Be(price.Value * 1.1);
    }

    [Theory, AutoData] // AutoData autogenerates test param values
    public void Should_convert_to_string(Price price)
    {
        var priceString = price.ToString();
        priceString.Should().Be(price.Value.ToString());
    }
}
