module MyBlazorApp.Utility.Http

open System.Net.Http

module HttpClient =
    let setBaseAddress uri (httpClient: HttpClient) = httpClient.BaseAddress <- uri

[<AbstractClass; Sealed>]
type UriHelper() =
    static member inline GetParametrizedUriString(baseAddress: string, p1Name: string, p1Value) =
        $"{baseAddress}?{p1Name}={p1Value}"

    static member inline GetParametrizedUriString(baseAddress: string, p1Name: string, p1Value, p2Name: string, p2Value) =
        $"{baseAddress}?{p1Name}={p1Value}&{p2Name}={p2Value}"

    static member inline GetParametrizedUriString(baseAddress: string, p1Name: string, p1Value, p2Name: string, p2Value, p3Name: string, p3Value) =
        $"{baseAddress}?{p1Name}={p1Value}&{p2Name}={p2Value}&{p3Name}={p3Value}"

    static member inline GetParametrizedUriString(baseAddress: string, p1Name: string, p1Value, p2Name: string, p2Value, p3Name: string, p3Value, p4Name: string, p4Value) =
        $"{baseAddress}?{p1Name}={p1Value}&{p2Name}={p2Value}&{p3Name}={p3Value}&{p4Name}={p4Value}"

    static member inline GetParametrizedUriString(baseAddress: string, p1Name: string, p1Value, p2Name: string, p2Value, p3Name: string, p3Value, p4Name: string, p4Value, p5Name: string, p5Value) =
        $"{baseAddress}?{p1Name}={p1Value}&{p2Name}={p2Value}&{p3Name}={p3Value}&{p4Name}={p4Value}&{p5Name}={p5Value}"

    static member inline GetParametrizedUriString(baseAddress: string, p1Name: string, p1Value, p2Name: string, p2Value, p3Name: string, p3Value, p4Name: string, p4Value, p5Name: string, p5Value,
                                        p6Name: string, p6Value) =
        $"{baseAddress}?{p1Name}={p1Value}&{p2Name}={p2Value}&{p3Name}={p3Value}&{p4Name}={p4Value}&{p5Name}={p5Value}&{p6Name}={p6Value}"