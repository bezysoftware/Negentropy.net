using FluentAssertions;

namespace Negentropy.Tests
{
    public class ReconcileTests
    {
        [Fact]
        public void ReconcileTerminatesWithNull()
        {
            Item[] items = [
                new ("eb6b05c2e3b008592ac666594d78ed83e7b9ab30f825b9b08878128f7500008c", 1678011277),
                new ("39b916432333e069a4386917609215cc688eb99f06fed01aadc29b1b4b92d6f0", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3bc", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3bd", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3be", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3bf", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c0", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c1", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c2", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c3", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c4", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c5", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c6", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c8", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c9", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3ca", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cb", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cc", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cd", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d0", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d1", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d2", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d3", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d4", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d5", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d6", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d7", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d8", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d9", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3da", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3db", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3dc", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3dd", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3de", 1678011277)
            ];

            var ne = new NegentropyBuilder(new NegentropyOptions()).AddRange(items).Build();

            var init = ne.Initiate();
            var result = ne.Reconcile(init);

            result.Query.Should().Be(string.Empty);
        }

        [Fact]
        public void HaveSomeNeedSome()
        {
            Item[] items = [
                new ("eb6b05c2e3b008592ac666594d78ed83e7b9ab30f825b9b08878128f7500008c", 1678011277),
                new ("39b916432333e069a4386917609215cc688eb99f06fed01aadc29b1b4b92d6f0", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3bc", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3bd", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3be", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3bf", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c0", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c1", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c2", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c3", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c4", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c5", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c6", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c8", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c9", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3ca", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cb", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cc", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cd", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d0", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d1", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d2", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d3", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d4", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d5", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d6", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d7", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d8", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d9", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3da", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3db", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3dc", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3dd", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3de", 1678011277)
            ];

            Item[] items2 = [
                new ("eb6b05c2e3b008592ac666594d78ed83e7b9ab30f825b9b08878128f7500008c", 1678011277),
                new ("39b916432333e069a4386917609215cc688eb99f06fed01aadc29b1b4b92d6f0", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3bc", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3bd", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3be", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3bf", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c0", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c1", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c2", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c3", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c4", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c5", 1678011277),
                new ("abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c6", 1678011277),
                new ("bbc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7", 1678011277),
                new ("cbc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7", 1678011277),
                new ("dbc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7", 1678011277),
                new ("ebc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7", 1678011277),
            ];

            var client = new NegentropyBuilder(new NegentropyOptions()).AddRange(items).Build();
            var server = new NegentropyBuilder(new NegentropyOptions()).AddRange(items2).Build();

            var init = client.Initiate();
            var serverResult = server.Reconcile(init);
            var result = client.Reconcile(serverResult.Query!);

            // verify intermediate results
            init.Should().Be("6186a091d70e20abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3be018ab006273abaa0c5a1759a7069fb4d640120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c1015f7c56695039382da4283f0aa978cde30120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c40134f7410d270c0cf7ea1882a3b7219acf0120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c601b2e06a675303a7656c759aa99c84c54f0120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c801098f62ce750b28bb99d18688b1a23e110120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3ca01378443b2bb69b1c17613cc620eeaca3c0120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cc0177f5ad96846fae737f9f7c4a90ddcbb10120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d00166c46e7240de606b2e376b83cf3d04980120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d20177cc22e5eeafb4f4a273cda0626050db0120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d401719a1a3a5146190e7eb84d3e52ac7b830120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d6014f6e4490344ad616c7e7064ecce868a90120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d8010b999dfb663e37a3676bcb5bd49b9ff80120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3da01d9820357cc97b12610b9b3450832968c0120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3dc01115152633e1ab6989c1c9616dc36fb300120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3de01b17772afa3c0d85bc81457a87b0a83d30000019b6e9972496614a88d32c8a008a1b78c");
            serverResult.Query.Should().Be("6186a091d70e20abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c6000120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c80201abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c60120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3ca02000120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cc02000120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d002000120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d202000120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d402000120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d602000120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d802000120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3da02000120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3dc02000120abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3de020000000205bbc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7cbc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7dbc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7eb6b05c2e3b008592ac666594d78ed83e7b9ab30f825b9b08878128f7500008cebc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7");

            result.Query.Should().Be(string.Empty);
            result.NeedIds.Should().BeEquivalentTo(
            [
                "bbc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7",
                "cbc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7",
                "dbc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7",
                "ebc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7"
            ]);
            result.HaveIds.Should().BeEquivalentTo(
            [
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c7",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c8",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3c9",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3ca",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cb",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cc",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3cd",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d0",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d1",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d2",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d3",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d4",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d5",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d6",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d7",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d8",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3d9",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3da",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3db",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3dc",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3dd",
                "abc81d58ebe3b9a87100d47f58bf15e9b1cbf62d38623f11d0f0d17179f5f3de"
            ]);
        }

        [Fact]
        public void LoadFromFile()
        {
            var items1 = File
                .ReadAllLines("input1.txt")
                .Select(x => x.Split(","))
                .Select(x => new Item(x[1], long.Parse(x[0])))
                .ToArray();

            var items2 = File
                .ReadAllLines("input2.txt")
                .Select(x => x.Split(","))
                .Select(x => new Item(x[1], long.Parse(x[0])))
                .ToArray();

            var ne1 = new NegentropyBuilder(new NegentropyOptions()).AddRange(items1).Build();
            var ne2 = new NegentropyBuilder(new NegentropyOptions()).AddRange(items2).Build();

            var init = ne1.Initiate();

            var result = ne2.Reconcile(init);
            result = ne1.Reconcile(result.Query);

            while (result.Query != string.Empty)
            {
                result = ne2.Reconcile(result.Query);
                result = ne1.Reconcile(result.Query);
            }

            result.NeedIds.Intersect(result.HaveIds).Should().BeEmpty();
        }
    }
}
