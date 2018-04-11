using System.Text;
using static DevOps.Primitives.TypeScript.JsonStringBuilderHelper;

namespace DevOps.Primitives.TypeScript
{
    public interface IJsonSerializable
    {
        StringBuilder GetJsonStringBuilder(
            ZeroPropertyBehavior zeroPropertyBehavior = ZeroPropertyBehavior.ReturnNull,
            byte indent = IndentZero,
            StringBuilder stringBuilder = null);
    }
}
