using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace ConsoleAppTestTgBot.Bot;

public class BotInitializer
{
    private TelegramBotClient _botClient;
    private CancellationTokenSource _cancellationTokenSource;

    public BotInitializer()
    {
        _botClient = new TelegramBotClient("6484640879:AAGtK-Kv5FfOltNVZklrmHxsvbAgi9304K8");
        _cancellationTokenSource = new CancellationTokenSource();
        
        Console.WriteLine("Выполнена инициализация Бота");
    }
    
    public void Start()
    {
        ReceiverOptions receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        BotRequestHandlers botRequestHandlers = new BotRequestHandlers();

        _botClient.StartReceiving(
            botRequestHandlers.HandleUpdateAsync,
            botRequestHandlers.HandlePollingErrorAsync,
            receiverOptions,
            _cancellationTokenSource.Token
        );
        
        Console.WriteLine("Бот запущен");
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
        
        Console.WriteLine("Бот остановлен");
    }
}