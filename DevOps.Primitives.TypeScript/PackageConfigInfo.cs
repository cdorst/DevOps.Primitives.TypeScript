using System;
using System.Collections.Generic;
using System.Text;
using static Common.Functions.CheckNullableEnumerationForAnyElements.NullableEnumerationAny;
using static DevOps.Primitives.TypeScript.JsonStringBuilderHelper;

namespace DevOps.Primitives.TypeScript
{
    public class PackageConfigInfo : IJsonSerializable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PackageConfigPerson Author { get; set; }
        public string Version { get; set; }
        public string Main { get; set; }
        public string Types { get; set; }
        public IEnumerable<string> Keywords { get; set; }
        public string Homepage { get; set; }
        public PackageConfigBugs Bugs { get; set; }
        public string License { get; set; }
        public IEnumerable<PackageConfigPerson> Contributors { get; set; }
        public IEnumerable<string> Files { get; set; }
        public IDictionary<string, string> Bin { get; set; }
        public IEnumerable<string> Man { get; set; }
        public IDictionary<PackageConfigDirectories, string> Directories { get; set; }
        public PackageConfigRepository Repository { get; set; }
        public IDictionary<PackageConfigScripts, string> Scripts { get; set; }
        public IDictionary<string, string> Config { get; set; }
        public IDictionary<string, string> Dependencies { get; set; }
        public IDictionary<string, string> DevDependencies { get; set; }
        public IDictionary<string, string> PeerDependencies { get; set; }
        public IEnumerable<string> BundledDependencies { get; set; }
        public IDictionary<string, string> OptionalDependencies { get; set; }
        public IDictionary<string, string> Engines { get; set; }
        public IEnumerable<string> Os { get; set; }
        public IEnumerable<string> Cpu { get; set; }
        public bool Private { get; set; }
        public IDictionary<string, string> PublishConfig { get; set; }
        public IDictionary<string, bool> Browser { get; set; }

        public StringBuilder GetJsonStringBuilder(
            ZeroPropertyBehavior zeroPropertyBehavior = ZeroPropertyBehavior.ReturnNull,
            byte indent = IndentZero,
            StringBuilder stringBuilder = null)
            => JsonObject(JsonProperties(), zeroPropertyBehavior, indent, stringBuilder);

        private IEnumerable<Func<StringBuilder, byte, StringBuilder>> JsonProperties()
        {
            /* required */                  yield return Json(nameof(Name), Name);
            if (NotNull(Description))       yield return Json(nameof(Description), Description);
            if (NotNull(Author))            yield return Json(nameof(Author), Author);
            /* required */                  yield return Json(nameof(Version), Version);
            if (NotNull(Main))              yield return Json(nameof(Main), Main);
            if (NotNull(Types))             yield return Json(nameof(Types), Types);
            if (Any(Keywords))              yield return Json(nameof(Keywords), Keywords);
            if (NotNull(Homepage))          yield return Json(nameof(Homepage), Homepage);
            if (NotNull(Bugs))              yield return Json(nameof(Bugs), Bugs);
            if (NotNull(License))           yield return Json(nameof(License), License);
            if (Any(Contributors))          yield return Json(nameof(Contributors), Contributors);
            if (Any(Files))                 yield return Json(nameof(Files), Files);
            if (Any(Bin))                   yield return Json(nameof(Bin), Bin);
            if (Any(Man))                   yield return Json(nameof(Man), Man);
            if (Any(Directories))           yield return Json(nameof(Directories), Directories);
            if (NotNull(Repository))        yield return Json(nameof(Repository), Repository);
            if (Any(Scripts))               yield return Json(nameof(Scripts), Scripts);
            if (Any(Config))                yield return Json(nameof(Config), Config);
            if (Any(Dependencies))          yield return Json(nameof(Dependencies), Dependencies);
            if (Any(DevDependencies))       yield return Json(nameof(DevDependencies), DevDependencies);
            if (Any(PeerDependencies))      yield return Json(nameof(PeerDependencies), PeerDependencies);
            if (Any(BundledDependencies))   yield return Json(nameof(BundledDependencies), BundledDependencies);
            if (Any(OptionalDependencies))  yield return Json(nameof(OptionalDependencies), OptionalDependencies);
            if (Any(Engines))               yield return Json(nameof(Engines), Engines);
            if (Any(Os))                    yield return Json(nameof(Os), Os);
            if (Private)                    yield return Json(nameof(Private), Private);
            if (Any(PublishConfig))         yield return Json(nameof(PublishConfig), PublishConfig);
            if (Any(Browser))               yield return Json(nameof(Browser), Browser);
        }
    }
}
