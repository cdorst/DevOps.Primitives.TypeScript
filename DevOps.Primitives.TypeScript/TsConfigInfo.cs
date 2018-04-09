using Common.EnumStringValues;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Common.Functions.CheckNullableEnumerationForAnyElements.NullableEnumerationAny;

namespace DevOps.Primitives.TypeScript
{
    public class TsConfigInfo
    {
        public TsConfigInfo() { }
        public TsConfigInfo(TsConfigTarget target, TsConfigModule module, IEnumerable<TsConfigLib> lib = null, bool allowJs = false, bool checkJs = false, TsConfigJsx? jsx = null, bool declaration = false, bool sourceMap = false, string outFile = null, string outDir = null, string rootDir = null, bool removeComments = false, bool noEmit = false, bool importHelpers = false, bool downLevelIteration = false, bool isolatedModules = false, bool strict = false, bool noImplicitAny = false, bool strictNullChecks = false, bool strictFunctionsTypes = false, bool strictPropertyInitialization = false, bool noImplicitThis = false, bool alwaysStrict = false, bool noUnusedLocals = false, bool noUnusedParameters = false, bool noImplicitReturns = false, bool noFallthroughCasesInSwitch = false, TsConfigModuleResolution? moduleResolution = null, string baseUrl = null, IDictionary<string, IEnumerable<string>> paths = null, IEnumerable<string> rootDirs = null, IEnumerable<string> typeRoots = null, IEnumerable<string> types = null, bool allowSyntheticDefaultImports = false, bool esModuleInterop = false, bool preserveSymlinks = false, string sourceRoot = null, string mapRoot = null, bool inlineSourceMap = false, bool inlineSources = false, bool experimentalDecorators = false, bool emitDecoratorMetadata = false, IEnumerable<string> includePaths = null)
        {
            Target = target;
            Module = module;
            Lib = lib;
            AllowJs = allowJs;
            CheckJs = checkJs;
            Jsx = jsx;
            Declaration = declaration;
            SourceMap = sourceMap;
            OutFile = outFile;
            OutDir = outDir;
            RootDir = rootDir;
            RemoveComments = removeComments;
            NoEmit = noEmit;
            ImportHelpers = importHelpers;
            DownLevelIteration = downLevelIteration;
            IsolatedModules = isolatedModules;
            Strict = strict;
            NoImplicitAny = noImplicitAny;
            StrictNullChecks = strictNullChecks;
            StrictFunctionsTypes = strictFunctionsTypes;
            StrictPropertyInitialization = strictPropertyInitialization;
            NoImplicitThis = noImplicitThis;
            AlwaysStrict = alwaysStrict;
            NoUnusedLocals = noUnusedLocals;
            NoUnusedParameters = noUnusedParameters;
            NoImplicitReturns = noImplicitReturns;
            NoFallthroughCasesInSwitch = noFallthroughCasesInSwitch;
            ModuleResolution = moduleResolution;
            BaseUrl = baseUrl;
            Paths = paths;
            RootDirs = rootDirs;
            TypeRoots = typeRoots;
            Types = types;
            AllowSyntheticDefaultImports = allowSyntheticDefaultImports;
            EsModuleInterop = esModuleInterop;
            PreserveSymlinks = preserveSymlinks;
            SourceRoot = sourceRoot;
            MapRoot = mapRoot;
            InlineSourceMap = inlineSourceMap;
            InlineSources = inlineSources;
            ExperimentalDecorators = experimentalDecorators;
            EmitDecoratorMetadata = emitDecoratorMetadata;
            IncludePaths = includePaths;
        }

        public TsConfigTarget Target { get; set; }
        public TsConfigModule Module { get; set; }
        public IEnumerable<TsConfigLib> Lib { get; set; }
        public bool AllowJs { get; set; }
        public bool CheckJs { get; set; }
        public TsConfigJsx? Jsx { get; set; }
        public bool Declaration { get; set; }
        public bool SourceMap { get; set; }
        public string OutFile { get; set; }
        public string OutDir { get; set; }
        public string RootDir { get; set; }
        public bool RemoveComments { get; set; }
        public bool NoEmit { get; set; }
        public bool ImportHelpers { get; set; }
        public bool DownLevelIteration { get; set; }
        public bool IsolatedModules { get; set; }
        public bool Strict { get; set; }
        public bool NoImplicitAny { get; set; }
        public bool StrictNullChecks { get; set; }
        public bool StrictFunctionsTypes { get; set; }
        public bool StrictPropertyInitialization { get; set; }
        public bool NoImplicitThis { get; set; }
        public bool AlwaysStrict { get; set; }
        public bool NoUnusedLocals { get; set; }
        public bool NoUnusedParameters { get; set; }
        public bool NoImplicitReturns { get; set; }
        public bool NoFallthroughCasesInSwitch { get; set; }
        public TsConfigModuleResolution? ModuleResolution { get; set; }
        public string BaseUrl { get; set; }
        public IDictionary<string, IEnumerable<string>> Paths { get; set; }
        public IEnumerable<string> RootDirs { get; set; }
        public IEnumerable<string> TypeRoots { get; set; }
        public IEnumerable<string> Types { get; set; }
        public bool AllowSyntheticDefaultImports { get; set; }
        public bool EsModuleInterop { get; set; }
        public bool PreserveSymlinks { get; set; }
        public string SourceRoot { get; set; }
        public string MapRoot { get; set; }
        public bool InlineSourceMap { get; set; }
        public bool InlineSources { get; set; }
        public bool ExperimentalDecorators { get; set; }
        public bool EmitDecoratorMetadata { get; set; }
        public IEnumerable<string> IncludePaths { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder()
                .Append("{")
                .AppendLine("  \"compilerOptions\": {")
                .AppendLine($"    \"target\": \"{Target.GetStringValue()}\",")
                .AppendLine($"    \"module\": \"{Module.GetStringValue()}\"");
            if (Any(Lib))
            {
                stringBuilder.Append(",").AppendLine("    \"lib\": [");
                var count = Lib.Count();
                for (int i = 0; i < count; i++)
                {
                    stringBuilder.AppendLine($"      \"{Lib.ElementAt(i).GetStringValue()}\"");
                    if (i != count - 1) stringBuilder.Append(",");
                }
                stringBuilder.AppendLine("    ]");
            }
            if (AllowJs) stringBuilder.Append(",").AppendLine("    \"allowJs\": true");
            if (CheckJs) stringBuilder.Append(",").AppendLine("    \"checkJs\": true");
            if (Jsx != null) stringBuilder.Append(",").AppendLine($"    \"jsx\": \"{Jsx.GetStringValue()}\"");
            if (Declaration) stringBuilder.Append(",").AppendLine("    \"declaration\": true");
            if (SourceMap) stringBuilder.Append(",").AppendLine("    \"sourceMap\": true");
            if (!string.IsNullOrEmpty(OutFile)) stringBuilder.Append(",").AppendLine($"    \"outFile\": \"{OutFile}\"");
            if (!string.IsNullOrEmpty(OutDir)) stringBuilder.Append(",").AppendLine($"    \"outDir\": \"{OutDir}\"");
            if (!string.IsNullOrEmpty(RootDir)) stringBuilder.Append(",").AppendLine($"    \"rootDir\": \"{RootDir}\"");
            if (RemoveComments) stringBuilder.Append(",").AppendLine("    \"removeComments\": true");
            if (NoEmit) stringBuilder.Append(",").AppendLine("    \"noEmit\": true");
            if (ImportHelpers) stringBuilder.Append(",").AppendLine("    \"importHelpers\": true");
            if (DownLevelIteration) stringBuilder.Append(",").AppendLine("    \"downlevelIteration\": true");
            if (IsolatedModules) stringBuilder.Append(",").AppendLine("    \"isolatedModules\": true");
            if (Strict) stringBuilder.Append(",").AppendLine("    \"strict\": true");
            if (NoImplicitAny) stringBuilder.Append(",").AppendLine("    \"noImplicitAny\": true");
            if (StrictNullChecks) stringBuilder.Append(",").AppendLine("    \"strictNullChecks\": true");
            if (StrictFunctionsTypes) stringBuilder.Append(",").AppendLine("    \"strictFunctionTypes\": true");
            if (StrictPropertyInitialization) stringBuilder.Append(",").AppendLine("    \"strictPropertyInitialization\": true");
            if (NoImplicitThis) stringBuilder.Append(",").AppendLine("    \"noImplicitThis\": true");
            if (AlwaysStrict) stringBuilder.Append(",").AppendLine("    \"alwaysStrict\": true");
            if (NoUnusedLocals) stringBuilder.Append(",").AppendLine("    \"noUnusedLocals\": true");
            if (NoUnusedParameters) stringBuilder.Append(",").AppendLine("    \"noUnusedParameters\": true");
            if (NoImplicitReturns) stringBuilder.Append(",").AppendLine("    \"noImplicitReturns\": true");
            if (NoFallthroughCasesInSwitch) stringBuilder.Append(",").AppendLine("    \"noFallthroughCasesInSwitch\": true");
            if (ModuleResolution != null) stringBuilder.Append(",").AppendLine($"    \"moduleResolution\": \"{ModuleResolution.GetStringValue()}\"");
            if (!string.IsNullOrEmpty(BaseUrl)) stringBuilder.Append(",").AppendLine($"    \"baseUrl\": \"{BaseUrl}\"");
            if (Any(Paths))
            {
                stringBuilder.Append(",").AppendLine("    \"paths\": {");
                var count = Paths.Count();
                for (int i = 0; i < count; i++)
                {
                    var pathEntry = Paths.ElementAt(i);
                    var paths = string.Join(", ", pathEntry.Value);
                    stringBuilder.AppendLine($"      \"{pathEntry.Key}\": [{paths}]");
                    if (i != count - 1) stringBuilder.Append(",");
                }
                stringBuilder.AppendLine("    }");
            }
            if (Any(RootDirs))
            {
                stringBuilder.Append(",").AppendLine("    \"rootDirs\": [");
                var count = RootDirs.Count();
                for (int i = 0; i < count; i++)
                {
                    stringBuilder.AppendLine($"      \"{RootDirs.ElementAt(i)}\"");
                    if (i != count - 1) stringBuilder.Append(",");
                }
                stringBuilder.AppendLine("    ]");
            }
            if (Any(TypeRoots))
            {
                stringBuilder.Append(",").AppendLine("    \"typeRoots\": [");
                var count = TypeRoots.Count();
                for (int i = 0; i < count; i++)
                {
                    stringBuilder.AppendLine($"      \"{TypeRoots.ElementAt(i)}\"");
                    if (i != count - 1) stringBuilder.Append(",");
                }
                stringBuilder.AppendLine("    ]");
            }
            if (Any(Types))
            {
                stringBuilder.Append(",").AppendLine("    \"types\": [");
                var count = Types.Count();
                for (int i = 0; i < count; i++)
                {
                    stringBuilder.AppendLine($"      \"{Types.ElementAt(i)}\"");
                    if (i != count - 1) stringBuilder.Append(",");
                }
                stringBuilder.AppendLine("    ]");
            }
            if (AllowSyntheticDefaultImports) stringBuilder.Append(",").AppendLine("    \"allowSyntheticDefaultImports\": true");
            if (EsModuleInterop) stringBuilder.Append(",").AppendLine("    \"esModuleInterop\": true");
            if (PreserveSymlinks) stringBuilder.Append(",").AppendLine("    \"preserveSymlinks\": true");
            if (!string.IsNullOrEmpty(SourceRoot)) stringBuilder.Append(",").AppendLine($"    \"sourceRoot\": \"{SourceRoot}\"");
            if (!string.IsNullOrEmpty(MapRoot)) stringBuilder.Append(",").AppendLine($"    \"mapRoot\": \"{MapRoot}\"");
            if (InlineSourceMap) stringBuilder.Append(",").AppendLine("    \"inlineSourceMap\": true");
            if (InlineSources) stringBuilder.Append(",").AppendLine("    \"inlineSources\": true");
            if (ExperimentalDecorators) stringBuilder.Append(",").AppendLine("    \"experimentalDecorators\": true");
            if (EmitDecoratorMetadata) stringBuilder.Append(",").AppendLine("    \"emitDecoratorMetadata\": true");
            if (Any(IncludePaths))
            {
                stringBuilder.AppendLine("  },").AppendLine("  \"include\": [");
                var count = IncludePaths.Count();
                for (int i = 0; i < count; i++)
                {
                    stringBuilder.AppendLine($"    \"{IncludePaths.ElementAt(i)}\"");
                    if (i != count - 1) stringBuilder.Append(",");
                }
                stringBuilder.AppendLine("    ]");
            }
            return stringBuilder
                .AppendLine("  }")
                .AppendLine("}")
                .AppendLine()
                .ToString();
        }
    }
}
