namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class RightExExpression : Expression {

        public Expression targetExp;

        public RightExExpression(Expression targetExp) {
            type = Type.RightEx;
            this.targetExp = targetExp;
        }

        public override string ToString() {
            return string.Format("{0}, ", targetExp);
        }

    }

}