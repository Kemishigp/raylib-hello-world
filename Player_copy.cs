using Raylib_cs;
using System.Numerics;

class Player: MovePlayer{
    public void Draw(){
        var PlayerRectangle = new Rectangle(430, 400, 20, 20);
        Raylib.DrawCircle((int)Position.X, (int)Position.Y,15, Color.BLUE);
    }
}

class MovePlayer{
    
    public Vector2 Position {get; set; } = new Vector2(0, 0);
    public Vector2 RiseFall(bool OnPlatform, Vector2 PlayerPosition, bool TouchBottom, int PlatformSpeed,int FallSpeed){
    var player = new Player();
    Vector2 possition = PlayerPosition;
        if (OnPlatform == true)
        {
            Vector2 Landing = possition;
            Landing.X = possition.X;
            Landing.Y += PlatformSpeed;
            possition = Landing;
        }
        if(OnPlatform == false && TouchBottom == true)
        {
            Vector2 Falling = possition;
            Falling.X = possition.X;
            Falling.Y += 0;
            possition = Falling;
        }
        
        if(OnPlatform == false && TouchBottom == false)
        {
            Vector2 Falling = possition;
            Falling.X = possition.X;
            Falling.Y += FallSpeed;
            possition = Falling;
        }
        return possition;
    }
}




// Player can move in 3 directions
// 1. Down - Activated by being airborne
// 2. Up - Player touches a platform
// 3. Stationary - Cause by landing at the bottom of the screen