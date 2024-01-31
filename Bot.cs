using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace TG_Shop_delivery
{
    internal class Bot
    {
        private static ITelegramBotClient botClient; // Клиент
        private static ReceiverOptions receiverOptions; // Указываем какие типы update будем получать

        static async Task tg_Bot(string botToken)
        {
            botClient = new TelegramBotClient(botToken); // Передаем токен
            receiverOptions = new ReceiverOptions()  // значения настройки боты
            {
                AllowedUpdates = new[]
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery,
                },
                ThrowPendingUpdates = true, // Обработка сообщений, которые пришли за врем офлайна (не обрабатывать)
            };

            using var cts = new CancellationTokenSource();

            // Обработка сообщений:
            botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);
            //Переменная в котороую перемещаем инф-ю о боте
            var me = await botClient.GetMeAsync();
            Console.WriteLine($"{DateTime.Now} || {me.Username} || Начало работы");
        }

        private static async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:

                        var message = update.Message;
                        var user = message.From;
                        var chat = message.Chat;


                        //...
                        break;
                    case UpdateType.CallbackQuery:
                        //...
                        break;





                }
            }


            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            // Тут создадим переменную, в которую поместим код ошибки и её сообщение 
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
