using GraphQL.Types;

namespace GraphQLDemo.Type
{
    public class ProductInputType : InputObjectGraphType
    {
        public ProductInputType()
        {
            Field<StringGraphType>("Id");
            Field<StringGraphType>("Name");
            Field<FloatGraphType>("Price");

        }
    }
}
