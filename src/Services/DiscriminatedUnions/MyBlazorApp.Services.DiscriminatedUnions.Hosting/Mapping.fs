namespace MyBlazorApp.Services.DiscriminatedUnions.Hosting

open MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1
open MyBlazorApp.Services.DiscriminatedUnions.Domain

module ImportantData =
    let toDto = function
        | ImportantData.NameAndAge(name, age) -> ImportantDataDto.NameAndAge(name, age)
        | ImportantData.PriceRangeAndCount(from, to', count) -> ImportantDataDto.PriceRangeAndCount(from, to', count)
        | ImportantData.Cart(items) -> ImportantDataDto.Cart(items)

    let fromDto = function
        | ImportantDataDto.NameAndAge(name, age) -> ImportantData.NameAndAge(name, age)
        | ImportantDataDto.PriceRangeAndCount(from, to', count) -> ImportantData.PriceRangeAndCount(from, to', count)
        | ImportantDataDto.Cart(items) -> ImportantData.Cart(items)