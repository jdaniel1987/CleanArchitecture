using AutoFixture.Xunit2;
using CleanArchitecture.Domain.ValueObjects;
using FluentAssertions;

namespace CleanArchitecture.Domain.Tests.ValueObjects;

public class PriceEurosTests
{
    [Theory, AutoData] // AutoData autogenerates test param values
    public void Should_create_price_in_euros(double priceInEuros)
    {
        var price = PriceEuros.Create(priceInEuros);

        price.Value.Should().Be(priceInEuros);
    }

    [Theory, AutoData] // AutoData autogenerates test param values
    public void Should_convert_to_string(PriceEuros price)
    {
        var priceString = price.ToString();
        priceString.Should().Be(price.Value.ToString());
    }
}
