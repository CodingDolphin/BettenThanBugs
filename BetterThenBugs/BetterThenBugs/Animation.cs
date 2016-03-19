using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace BetterThenBugs
{
    class Animation
    {
        Texture2D AnimationsSprite;
        public Vector2 AnimationsPosition;
        public Rectangle SourceRectangle;
        public Rectangle DestinationRectangle;
        public SpriteEffects Direction;

        //SpriteMaße

        public int CurrentFrame;
        public int MaxFrame;

        int FrameWidth;
        int FrameHeight;

        //Framecycle

        public bool AnimationActive = false;

        float FrameCycle;
        float CurrentGameTime;
        
        //Konstruktor

        public Animation()
        {
        }

        //KlassenMethoden

        public void Initialize(Vector2 animationsPosition,float frameCycle, int currentFrame, int MaxFrame, int frameHeight, int frameWidth)
        {
            this.AnimationsPosition = animationsPosition;
            this.CurrentFrame = currentFrame;
            this.MaxFrame = MaxFrame;
            this.FrameHeight = frameHeight;
            this.FrameWidth = frameWidth;
            this.FrameCycle = frameCycle;

            DestinationRectangle = new Rectangle((int)AnimationsPosition.X, (int)AnimationsPosition.Y,FrameWidth,FrameHeight);
            SourceRectangle = new Rectangle(CurrentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
        }

        //LoadContent

        public void LoadContent(ContentManager content, string assetName)
        {
            AnimationsSprite = content.Load<Texture2D>(assetName);
        }

        //Update Frames

        public void Update(GameTime gameTime)
        {
            if (!AnimationActive)
            {
                SourceRectangle = new Rectangle(CurrentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
                DestinationRectangle = new Rectangle((int)AnimationsPosition.X, (int)AnimationsPosition.Y, FrameWidth, FrameHeight);
                return;
            }  
        
            CurrentGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (CurrentGameTime >= FrameCycle)
            {
                CurrentFrame++;

                if (CurrentFrame == MaxFrame)
                {
                    CurrentFrame = 0;
                }

                  CurrentGameTime = 0;
            }

           SourceRectangle = new Rectangle(CurrentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
           DestinationRectangle = new Rectangle((int)AnimationsPosition.X, (int)AnimationsPosition.Y, FrameWidth, FrameHeight);
           
        }
          
        //DrawMethode

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(AnimationsSprite,new Vector2(DestinationRectangle.X,DestinationRectangle.Y), SourceRectangle,Color.White,0,Vector2.Zero,1f,Direction,0);
            spriteBatch.End();
        }
    }
}
