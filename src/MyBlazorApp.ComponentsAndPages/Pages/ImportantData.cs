using System;
using static MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1;

namespace MyBlazorApp.ComponentsAndPages.Pages.DiscriminatedUnions
{
    public record ImportantData
    {
        private protected ImportantData() { }

        public record NameAndAge(string Name, int Age) : ImportantData;
        public record PriceRangeAndCount(int RangeFrom, int RangeTo, int Count) : ImportantData;
        public record Cart(string[] Items) : ImportantData;

        public static ImportantData FromDto(ImportantDataDto dto) => dto switch
        {
            ImportantDataDto.NameAndAge nameAndAge                 => new NameAndAge(nameAndAge.Name, nameAndAge.Age),
            ImportantDataDto.PriceRangeAndCount priceRangeAndCount => new PriceRangeAndCount(priceRangeAndCount.RangeFrom, priceRangeAndCount.RangeTo, priceRangeAndCount.Count),
            ImportantDataDto.Cart cart                             => new Cart(cart.Items),
            _                                                      => throw new NotSupportedException("")
        };
    }
}