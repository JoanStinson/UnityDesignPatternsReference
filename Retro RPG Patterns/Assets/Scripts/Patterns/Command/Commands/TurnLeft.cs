namespace JGM.Patterns.Command
{
    public class TurnLeft : Command
    {
        private readonly BikeController _controller;

        public TurnLeft(BikeController controller)
        {
            _controller = controller;
        }

        public override void Execute()
        {
            _controller.Turn(BikeController.Direction.Left);
        }
    }
}