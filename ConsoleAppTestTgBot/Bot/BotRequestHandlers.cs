using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleAppTestTgBot.Bot;

public class BotRequestHandlers
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        long chatId = 0;
        int messageFromUserId = 0;
        string textData = "";

        try
        {
            if (update.Type == UpdateType.Message)
            {
                chatId = update.Message.Chat.Id;
                messageFromUserId = update.Message.MessageId;
                textData = update.Message.Text;

                Console.WriteLine(
                    $"ВХОДЯЩЕЕ СОООБЩЕНИЕ chatId = {chatId}; messageId = {messageFromUserId}; text = {textData} ");

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Сообщение получено: " + textData,
                    cancellationToken: cancellationToken
                );
            }
        }
        catch (Exception e)
        {
            await botClient.DeleteMessageAsync(
                chatId: chatId,
                messageId: messageFromUserId,
                cancellationToken: cancellationToken
            );

            Console.WriteLine($"ОШИБКА chatId = {chatId}; text = {e.Message}");
        }
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine($"Ошибка поймана в методе HandlePollingErrorAsync, {errorMessage}");
        return Task.CompletedTask;
    }
}