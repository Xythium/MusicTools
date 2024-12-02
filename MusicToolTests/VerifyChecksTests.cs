using System.Threading.Tasks;
using NUnit.Framework;
using VerifyNUnit;

namespace MusicToolTests;

[TestFixture]
public class VerifyChecksTests
{
    [Test]
    public Task Run() =>
        VerifyChecks.Run();
}