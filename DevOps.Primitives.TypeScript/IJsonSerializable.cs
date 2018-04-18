using System.Text;
using static DevOps.Primitives.TypeScript.JsonStringBuilderHelper;

namespace DevOps.Primitives.TypeScript
{
    public interface IJsonSerializable
    {
        StringBuilder GetJsonStringBuilder(
            EmptyResponseBehavior behavior = EmptyResponseBehavior.Default,
            byte indent = IndentZero,
            StringBuilder stringBuilder = null);
    }
}
