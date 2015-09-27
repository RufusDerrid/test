using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer2.LevelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Platformer2
{
    public class GameplayScreen : GameScreen
    {
        private Player _player;
        private Alterverse _alterverse;
        private LevelView _levelView;
        //private Map _map;
        private List<AlternativeIcon> _alterIcons;
        
        private bool _pause;
        private double _tempTimer;
        private Tile SelectedTile;

        private MouseState _prevMouseState;
        private MouseState _mouseState;
        private List<Tile> _accessedTiles;

        private ContentManager _content;

        public override void LoadContent()
        {
            base.LoadContent();

            _content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            XmlManager<Player> playerLoader = new XmlManager<Player>();
            _player = playerLoader.Load("Content/Data/Gameplay/Player.xml");
            _player.LoadContent();

            _alterverse = new Alterverse(OnAlternativeChange);
            _alterverse.LoadContent();

            _levelView = new LevelView(_alterverse.CurrentLevel);
            _levelView.LoadContent();

            _alterIcons = new List<AlternativeIcon>();
            AlternativeIcon altIcon = new AlternativeIcon(new Vector2(100, 50), "1");
            altIcon.LoadContent();
            altIcon.IsActive = true;
            _alterIcons.Add(altIcon);

            //XmlManager<Map> mapLoader = new XmlManager<Map>();
            //_map = mapLoader.Load("Load/Gameplay/Maps/Map1.xml");
            //_map.LoadContent();

            _mouseState = Mouse.GetState();

            _accessedTiles = new List<Tile>();
        }

        private void OnAlternativeChange(Level level)
        {
            _levelView.ChangeAlternative(level);
            _player.ActivateGravity = true;

            if(_alterverse.CurrentAlternative > _alterIcons.Count-1)
            {
                AlternativeIcon newAltIcon = new AlternativeIcon(_alterIcons[_alterIcons.Count - 1].Position + new Vector2(100, 0),
                    (_alterverse.CurrentAlternative+1).ToString());
                newAltIcon.LoadContent();
                _alterIcons.Add(newAltIcon);
            }

            foreach(AlternativeIcon alterIcon in _alterIcons)
            {
                alterIcon.IsActive = false;
            }

            _alterIcons[_alterverse.CurrentAlternative].IsActive = true;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            _player.UnloadContent();
            _alterverse.UnloadContent();
            foreach(AlternativeIcon altIcon in _alterIcons)
            {
                altIcon.UnloadContent();
            }
            _alterIcons.Clear();
            //_map.UnloadContent();
            _content.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Entity e = _player;
            //_map.UpdateCollision(ref e);

            //_accessedTiles.Clear();
            //foreach (var layer in _map.Layer)
            //{
            //    foreach (var tile in layer.UnderlayTilesList)
            //    {
            //        Rectangle tempRect = new Rectangle((int)tile.Position.X, (int)tile.Position.Y, tile.SourceRect.Width, tile.SourceRect.Height);
            //        if (tempRect.Intersects(_player.ForceField))
            //            _accessedTiles.Add(tile);
            //    }

            //    foreach (var tile in layer.OverlayTilesList)
            //    {
            //        Rectangle tempRect = new Rectangle((int)tile.Position.X, (int)tile.Position.Y, tile.SourceRect.Width, tile.SourceRect.Height);
            //        if (tempRect.Intersects(_player.ForceField))
            //            _accessedTiles.Add(tile);
            //    }
            //}

            //if (InputManager.Instance.KeyPressed(Keys.Space))
            //    _pause = !_pause;

            //if (!_pause)
            //{
                _player.Update(gameTime);
                Entity e = _player;
                _alterverse.Update(gameTime, ref e);
                _levelView.Update(gameTime);
            
                //_map.Update(gameTime, ref _player);
            //}
            //else
            //{
            //    _prevMouseState = _mouseState;
            //    _mouseState = Mouse.GetState();

            //    if (_mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            //    {
            //        Point mousePos = _mouseState.Position;
            //        foreach (var tile in _accessedTiles)
            //        {
            //            if (mousePos.X > tile.Position.X && mousePos.X < tile.Position.X + tile.SourceRect.Width
            //                && mousePos.Y > tile.Position.Y && mousePos.Y < tile.Position.Y + tile.SourceRect.Height)
            //            {

            //                if (SelectedTile == null)
            //                {
            //                    tile.Selected = true;
            //                    SelectedTile = tile;
            //                    break;
            //                }
            //                else if (SelectedTile == tile)
            //                {
            //                    SelectedTile.Selected = false;
            //                    SelectedTile = null;
            //                    break;
            //                }
            //                else
            //                {
            //                    int chanse = SelectedTile.Chanse;
            //                    SelectedTile.Chanse = 0;
            //                    tile.Chanse += chanse;
            //                    SelectedTile.Selected = false;
            //                    SelectedTile = null;
            //                }
            //            }
            //        }
            //    }
            //}

            _tempTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (_tempTimer >= 1)
            {
                _tempTimer = 0;
            }

            if(_player.Image.Scale.X <= 0.0f)
            {
                ScreenManager.Instance.ChangeScreens("GameoverScreen");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //_map.Draw(spriteBatch, "Underlay", ref _accessedTiles, _pause);
            _levelView.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            foreach(AlternativeIcon altIcon in _alterIcons)
            {
                altIcon.Draw(spriteBatch);
            }
            //_map.Draw(spriteBatch, "Overlay", ref _accessedTiles, _pause);
        }
    }
}
