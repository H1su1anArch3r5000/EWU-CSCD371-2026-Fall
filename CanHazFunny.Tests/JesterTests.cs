using System;
using System.Threading.Tasks;

using CanHazFunny.Core;
using CanHazFunny.Output;
using CanHazFunny.Services;

using Moq;

using Xunit;

namespace CanHazFunny.Tests;

public sealed class JesterTests
{
    [Fact]
    public async Task TellJokeAsync_SkipsChuckNorrisJokes()
    {
        var jokeService = new Mock<IJokeBuilder>();
        var output = new Mock<IJokeOutputStringer>();

        jokeService.SetupSequence(s => s.GetJokeAsync())
            .ReturnsAsync("Chuck Norris counted to infinity. Twice.")
            .ReturnsAsync("A SQL query walks into a bar...");

        var jester = new Jester(jokeService.Object, output.Object);

        await jester.TellJoke();

        output.Verify(o => o.Write("A SQL query walks into a bar..."), Times.Once);
    }

    [Fact]
    public void Constructor_ThrowsOnNullDependencies()
    {
        var output = new Mock<IJokeOutputStringer>();

        Assert.Throws<ArgumentNullException>(() => new Jester(null!, output.Object));
        Assert.Throws<ArgumentNullException>(() => new Jester(new Mock<IJokeBuilder>().Object, null!));
    }
}
