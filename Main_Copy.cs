using Raylib_cs;
using System.Numerics;

namespace HelloWorld
{
    static class Program
    {
        public static void Main()
        {
// ******** CONSTANTS & VARIABLES
            var ScreenHeight = 480;
            var ScreenWidth = 800;
            bool GameOver = false;
            bool OnPlatform = false;
            int IncreaseAt = 17;
            bool TouchBottom = true;
            var Random = new Random();
            int PlayerSpeed = 5;
            int Level = 1;
            int FallSpeed = 4;
            Raylib.InitWindow(ScreenWidth, ScreenHeight, "PROJECT");
            Raylib.SetTargetFPS(60);
            int score = 0;
//********** LISTS
            // Platform List
            var PlatformList = new List<Platforms>();
            // Trigger List
            var TriggerList = new List<Rectangle>();
            // Point Tracker List
            var GapList = new List<Platforms>();
// ******** POSITIONS 
            // Define the Players Starting position & Velocity
            var PlayerPosition = new Vector2(ScreenWidth/2,450);
            Vector2 Velocity = new Vector2(0,-5);
            // Defines Position and Speed for Starting Platform
            int StartPlatY = ScreenHeight;
            int PlatformSpeed = -2;
            Rectangle StartingPlatform = new Rectangle(0,StartPlatY,600,10);
            TriggerList.Add(StartingPlatform); 
//********** Game execution
            while (!Raylib.WindowShouldClose())
            {
                if(GameOver == false){
                // Defines the position for Platforms in Platform List
                StartingPlatform.y=StartPlatY;
                int PositionX = Random.Next(-450, 0);
                var LeftPlatformPosition = new Vector2(PositionX , ScreenHeight);
                var RightPlatformPosition = new Vector2(PositionX+640, ScreenHeight);
                var GapPosition = new Vector2(PositionX+600,ScreenHeight);
                
                // Define the Trigger and Platform
                Rectangle TriggerPlatform = new Rectangle(0,0,ScreenWidth,ScreenHeight-150);
        //----- Raylib.DrawRectangle((int)TriggerPlatform.x,(int)TriggerPlatform.y,(int)TriggerPlatform.width,(int)TriggerPlatform.height,Color.GREEN);
        //----- Raylib.DrawRectangle((int)StartingPlatform.x,(int)StartingPlatform.y,(int)StartingPlatform.width,(int)StartingPlatform.height,Color.BLACK);

// ************ Foreach loop creates the Game platforms and defines it properties
              // If statement check for collisions between two invisible platforms
                foreach(var obj in TriggerList){
                    if (Raylib.CheckCollisionRecs(StartingPlatform,TriggerPlatform))
                    {
                        var LeftPlatform = new Platforms();
                        LeftPlatform.Position = LeftPlatformPosition;
                        LeftPlatform.Velocity = new Vector2(0,PlatformSpeed);
                        PlatformList.Add(LeftPlatform);
                        
                        var RightPlatform = new Platforms();
                        RightPlatform.Position = RightPlatformPosition;
                        RightPlatform.Velocity = new Vector2(0,PlatformSpeed);
                        PlatformList.Add(RightPlatform);

                        // Adds a rectangle to the gap between lines to keep track of score
                        var Gap = new Platforms();
                        Gap.Position = GapPosition;
                        Gap.Velocity = new Vector2(0,PlatformSpeed);
                        GapList.Add(Gap);
                        StartPlatY = ScreenHeight;
                    }
                }
                // Foreach loop adds and removes invisible trigger platforms from list
                foreach(var obj in TriggerList.ToList()){
                    if (Raylib.CheckCollisionRecs(StartingPlatform,TriggerPlatform))
                    {
                        TriggerList.Remove(obj);
                        TriggerList.Add(obj);
                    }
                }

                // Moves the trigger platform
                StartPlatY += PlatformSpeed;
                // Moves the Real Platforms
                foreach (var obj in PlatformList) {
                    obj.Draw();
                    obj.MoveVertical();
                }
                foreach (var obj in GapList) {
                    // obj.Draw();
                    obj.MoveVertical();
                }

//*************Create the player
                var player = new Player();
                player.Position = PlayerPosition;
                var PlayerRectangle = new Rectangle((int)PlayerPosition.X,(int)PlayerPosition.Y, 15,15);
                // Move player left & right
                player.Draw();
                // Move the player left-right
                if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) {
                    PlayerPosition.X +=PlayerSpeed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) {
                    PlayerPosition.X -=PlayerSpeed;
                }

            // Check to see if the player is on a platform
                foreach (var obj in PlatformList) {
                    if (Raylib.CheckCollisionRecs(new Rectangle((int)obj.Position.X,(int)obj.Position.Y,600,1), PlayerRectangle))
                    {
                        OnPlatform = true;
                        break;
                    }
                    else
                    {
                        OnPlatform = false;
                    }
                }
            // Check to see if the player is at the bottom or top
                if(PlayerPosition.Y >= ScreenHeight-PlayerRectangle.height)
                {
                    TouchBottom = true;
                }
                else
                {
                    TouchBottom = false;
                }
                if(PlayerPosition.Y <= 0)
                {
                    GameOver = true;
                }
                // Every time the player goes through a gap they earn a point
                foreach(var obj in GapList.ToList()){

    //----------------- Raylib.DrawRectangle((int)obj.Position.X,(int)obj.Position.Y,40,10,Color.BLACK);
                    if(Raylib.CheckCollisionRecs(new Rectangle((int)obj.Position.X,(int)obj.Position.Y,40, 10), PlayerRectangle))
                    {    
                        score +=1;
                        GapList.Remove(obj);
                    }
                }
                
                int mis = score+1;
                int Number = IncreaseAt%(mis);
                if(Number == IncreaseAt){
                    IncreaseAt = Number*2;
                    PlatformSpeed-=1;
                    PlayerSpeed +=1;
                    Level+=1;
                    FallSpeed+=1;
                }
                Console.WriteLine($"NUMBERER: {Number} SCORE:{score} MIS: {mis}");
                
                // Draw the Score
                Raylib.DrawText($"SCORE: {score}",12,12,15,Color.BLACK);
                Raylib.DrawText($"LEVEL: {Level}",710,12,15,Color.BLACK);
                // Calls the RiseFall function used to move the player vertically
                Vector2 gravity = player.RiseFall(OnPlatform, PlayerPosition,TouchBottom,PlatformSpeed,FallSpeed);
                PlayerPosition = gravity;
            }
                else 
                {
                    Raylib.DrawText($"GAME OVER\n SCORE:{score}",(ScreenWidth/2)-75,(ScreenHeight/2)-45,30,Color.BLACK);
                }
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
        }
    }
}


