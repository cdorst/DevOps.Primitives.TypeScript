using System.Text;
using static DevOps.Primitives.TypeScript.JsonStringBuilderHelper;

namespace DevOps.Primitives.TypeScript
{
    internal interface IJsonSerializable
    {
        StringBuilder GetJsonStringBuilder(
            in EmptyResponseBehavior behavior = EmptyResponseBehavior.Default,
            in byte indent = IndentZero,
            StringBuilder stringBuilder = default);
    }
}
