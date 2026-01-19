using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MeuPong;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;

    private SpriteBatch _spriteBatch;

    // Textura de 1x1 pixel para desenhar formas simples
    private Texture2D _pixelTextura;

    // Dimensões da tela
    private int _screenWidth = 800;
    private int _screenHeight = 600;

    // Raquete do jogador
    private Rectangle _player;
    private int _playerSpeed = 8;

    // Raquete do CPU
    private Rectangle _cpu;
    private int _cpuSpeed = 3;

    // Bola do jogo
    private Rectangle _ball;
    private int _ballSpeedX = 5;
    private int _ballSpeedY = 5;

    // Placar do jogo
    private int _playerScore = 0;
    private int _cpuScore = 0;

    public Game1()
    {
        // Construtor da classe Game1 
        // Aqui fazemos a configuração inicial do jogo
        _graphics = new GraphicsDeviceManager(this);
    }


    protected override void Initialize()
    {
        // Aqui inicializamos os componentes do jogo
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = _screenWidth;
        _graphics.PreferredBackBufferHeight = _screenHeight;
        _graphics.ApplyChanges();

        // Define o título da janela do jogo
        Window.Title = "Pong Game - SENAI";

        _player = new Rectangle(
            30, // posição X da raquete do jogador
            (_screenHeight / 2) - 50, // posição Y da raquete do jogador
            15, // largura da raquete do jogador
            100 // altura da raquete do jogador
        );

        _cpu = new Rectangle(
            _screenWidth - 45, // posição X da raquete do CPU
            (_screenHeight / 2) - 50, // posição Y da raquete do CPU
            15, // largura da raquete do CPU
            100 // altura da raquete do CPU
        );

        _ball = new Rectangle(
            (_screenWidth / 2) - 10, // posição X da bola
            (_screenHeight / 2) - 10, // posição Y da bola
            20, // largura da bola
            20 // altura da bola
        );

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Aqui carregamos o conteúdo do jogo (imagens, sons, etc.)
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Criar uma textura de 1x1 pixel branca
        _pixelTextura = new Texture2D(GraphicsDevice, 1, 1);
        _pixelTextura.SetData(new[] { Color.White });

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        // Logica do jogo - roda 60px por segundo por padrão
        // Fecha o jogo se o botão Back do controle for pressionado ou a tecla Escape
        // Aqui processamos os entradas, fisica, colisoes, etc.
        // Sair com o ESC
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Input do jogador
        var keyboard = Keyboard.GetState();

        // mover pra cima
        if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            _player.Y -= _playerSpeed;

        // mover pra baixo
        if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
            _player.Y += _playerSpeed;

        // Limitar a raquete do jogador dentro da tela
        if (_player.Y < 0)
            _player.Y = 0;
        if (_player.Y > _screenHeight - _player.Height)
            _player.Y = _screenHeight - _player.Height;

        // movimento da bola
        _ball.X += _ballSpeedX;
        _ball.Y += _ballSpeedY;

        // Colisao com topo e fundo
        if (_ball.Y <= 0 || _ball.Y >= _screenHeight - _ball.Height)
        {
            _ballSpeedY *= -1; // Inverte a direção vertical da bola
        }

        // Colisao com raquete do jogador
        if (_ball.Intersects(_player))
        {
            _ballSpeedX *= -1; // Inverte a direção horizontal da bola
        }

        // Colisao com raquete do CPU
        if (_ball.Intersects(_cpu))
        {
            _ballSpeedX *= -1; // Inverte a direção horizontal da bola
        }

        // ================== IA do CPU ==================
        // Move a raquete do CPU em direção à bola

        if (_ball.Y + (_ball.Height / 2) < _cpu.Y + (_cpu.Height / 2))
            _cpu.Y -= _cpuSpeed;
        else if (_ball.Y + (_ball.Height / 2) > _cpu.Y + (_cpu.Height / 2))
            _cpu.Y += _cpuSpeed;

        // Limitar a raquete do CPU dentro da tela
        if (_cpu.Y < 0)
            _cpu.Y = 0;
        if (_cpu.Y > _screenHeight - _cpu.Height)
            _cpu.Y = _screenHeight - _cpu.Height;

        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Limpa a tela com uma cor
        GraphicsDevice.Clear(Color.Black);

        // Inicia o desenho
        _spriteBatch.Begin();

        // Desenhar a raquete do jogador
        _spriteBatch.Draw(_pixelTextura, _player, Color.White);

        // Desenhar a raquete do CPU
        _spriteBatch.Draw(_pixelTextura, _cpu, Color.White);

        // Desenhar a bola
        _spriteBatch.Draw(_pixelTextura, _ball, Color.White);

        // Finaliza o desenho
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
