using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Ultra_Saver;
using Xunit;

namespace tests;


public class ExtensionsServiceTests
{

    [Theory]
    [MemberData(nameof(mapAppliesFunctionToAllCharactersCases))]
    public void mapAppliesFunctionToAllCharacters(string expected, string subject, Func<char, string> function)
    {
        Assert.Equal(expected, subject.map(function));
    }

    public static IEnumerable<object[]> mapAppliesFunctionToAllCharactersCases => new List<Object[]> {
        new object[] {"", "whatever", (char e) => ""},
        new object[] {"CAP", "Cap", (char e) => e.ToString().ToUpper()},
        new object[] {".a.b.c", "abc", (char e) => $".{e}"},
        new object[] {".a..b..c.", "abc", (char e) => $".{e}."},
        new object[] {@".*../..-..?..\.", @"*/-?\", (char e) => $".{e}."},
    };

    [Fact]
    public void nullIfEmailClaimNotFound()
    {
        Assert.Null(new ClaimsIdentity().getEmailFromClaim());
    }



}