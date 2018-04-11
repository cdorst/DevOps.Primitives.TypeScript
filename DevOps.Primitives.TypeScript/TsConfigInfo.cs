using Common.EnumStringValues;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DevOps.Primitives.TypeScript.JsonStringBuilderHelper;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    public class TsConfigInfo
    {
        private const string CompilerOptions = nameof(CompilerOptions);

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
            Include = includePaths;
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
        public IEnumerable<string> Include { get; set; }

        public override string ToString()
            => new StringBuilder()
                .Append(OpenCurlyBrace)
                .AppendIndentedLine()
                .AppendProperty(CompilerOptions)
                .Append(OpenCurlyBrace)
                .AppendStringProperty(nameof(Target), Target.GetStringValue(), IndentTwo, appendComma: false)
                .AppendStringProperty(nameof(Module), Module.GetStringValue(), IndentTwo)
                .ConditionallyAppendArray(nameof(Lib), Lib.Select(lib => lib.GetStringValue()), IndentTwo)
                .ConditionallyAppendBool(nameof(AllowJs), AllowJs, IndentTwo)
                .ConditionallyAppendBool(nameof(CheckJs), CheckJs, IndentTwo)
                .ConditionallyAppendString(nameof(Jsx), Jsx.GetStringValue(), IndentTwo)
                .ConditionallyAppendBool(nameof(Declaration), Declaration, IndentTwo)
                .ConditionallyAppendBool(nameof(SourceMap), SourceMap, IndentTwo)
                .ConditionallyAppendString(nameof(OutFile), OutFile, IndentTwo)
                .ConditionallyAppendString(nameof(OutDir), OutDir, IndentTwo)
                .ConditionallyAppendString(nameof(RootDir), RootDir, IndentTwo)
                .ConditionallyAppendBool(nameof(RemoveComments), RemoveComments, IndentTwo)
                .ConditionallyAppendBool(nameof(NoEmit), NoEmit, IndentTwo)
                .ConditionallyAppendBool(nameof(ImportHelpers), ImportHelpers, IndentTwo)
                .ConditionallyAppendBool(nameof(DownLevelIteration), DownLevelIteration, IndentTwo)
                .ConditionallyAppendBool(nameof(IsolatedModules), IsolatedModules, IndentTwo)
                .ConditionallyAppendBool(nameof(Strict), Strict, IndentTwo)
                .ConditionallyAppendBool(nameof(NoImplicitAny), NoImplicitAny, IndentTwo)
                .ConditionallyAppendBool(nameof(StrictNullChecks), StrictNullChecks, IndentTwo)
                .ConditionallyAppendBool(nameof(StrictFunctionsTypes), StrictFunctionsTypes, IndentTwo)
                .ConditionallyAppendBool(nameof(StrictPropertyInitialization), StrictPropertyInitialization, IndentTwo)
                .ConditionallyAppendBool(nameof(NoImplicitThis), NoImplicitThis, IndentTwo)
                .ConditionallyAppendBool(nameof(AlwaysStrict), AlwaysStrict, IndentTwo)
                .ConditionallyAppendBool(nameof(NoUnusedLocals), NoUnusedLocals, IndentTwo)
                .ConditionallyAppendBool(nameof(NoUnusedParameters), NoUnusedParameters, IndentTwo)
                .ConditionallyAppendBool(nameof(NoImplicitReturns), NoImplicitReturns, IndentTwo)
                .ConditionallyAppendBool(nameof(NoFallthroughCasesInSwitch), NoFallthroughCasesInSwitch, IndentTwo)
                .ConditionallyAppendString(nameof(ModuleResolution), ModuleResolution?.GetStringValue(), IndentTwo)
                .ConditionallyAppendString(nameof(BaseUrl), BaseUrl, IndentTwo)
                .ConditionallyAppendObject(nameof(Paths), Paths, IndentTwo)
                .ConditionallyAppendArray(nameof(RootDirs), RootDirs, IndentTwo)
                .ConditionallyAppendArray(nameof(TypeRoots), TypeRoots, IndentTwo)
                .ConditionallyAppendArray(nameof(Types), Types, IndentTwo)
                .ConditionallyAppendBool(nameof(AllowSyntheticDefaultImports), AllowSyntheticDefaultImports, IndentTwo)
                .ConditionallyAppendBool(nameof(EsModuleInterop), EsModuleInterop, IndentTwo)
                .ConditionallyAppendBool(nameof(PreserveSymlinks), PreserveSymlinks, IndentTwo)
                .ConditionallyAppendString(nameof(SourceRoot), SourceRoot, IndentTwo)
                .ConditionallyAppendString(nameof(MapRoot), MapRoot, IndentTwo)
                .ConditionallyAppendBool(nameof(InlineSourceMap), InlineSourceMap, IndentTwo)
                .ConditionallyAppendBool(nameof(InlineSources), InlineSources, IndentTwo)
                .ConditionallyAppendBool(nameof(ExperimentalDecorators), ExperimentalDecorators, IndentTwo)
                .ConditionallyAppendBool(nameof(EmitDecoratorMetadata), EmitDecoratorMetadata, IndentTwo)
                .AppendLine(IndentOne, CloseCurlyBrace)
                .ConditionallyAppendArray(nameof(Include), Include)
                .AppendLine(CloseCurlyBrace)
                .AppendLine()
                .ToString();
    }
}
