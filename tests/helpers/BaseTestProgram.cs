using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ultra_Saver.Tests;

public class BaseTestProgram : IClassFixture<TestWebAuthFactory<Program>>
{

    internal readonly TestWebAuthFactory<Program> _factory;

    internal BaseTestProgram(TestWebAuthFactory<Program> factory)
    {
        _factory = factory;

    }
}