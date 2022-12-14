using Raylib_cs;
using System.Numerics;

abstract class MovementVertical{
    public Vector2 Position {get; set; } = new Vector2(0, 0);
    public Vector2 Velocity {get; set; } = new Vector2(0, 0);
    // Y-VALUE POSITION OF ROCKS AND GEMS ARE INCREASING AT A CONSTANT RATE (GRAVITY)
    virtual public void Draw() {
        // Base game objects do not have anything to draw
    }
    public void MoveVertical(){
        Vector2 NewPosition = Position;
        NewPosition.X += Velocity.X;
        NewPosition.Y += Velocity.Y;
        Position = NewPosition;
    }
}

class Platforms: MovementVertical{
    // DRAW THE PLATFORMS
    override public void Draw() {
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y,600,10, Color.RED);
    }
}