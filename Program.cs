using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;

using Telegram.Bot.Exceptions;
using System.Threading;
using System;

namespace TG_Shop_delivery
{
    internal class Program
    {
        private static ITelegramBotClient botClient; // Клиент
        private static ReceiverOptions receiverOptions; // Указываем какие типы update будем получать

        static async Task Main()
        {
            botClient = new TelegramBotClient("6397637971:AAE7hqTcya-5M1adxUvYLDbuktVGh-vTG5g"); // Передаем токен
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

            await Task.Delay(-1);
        }

        private static async Task UpdateHandler(ITelegramBotClient client, Telegram.Bot.Types.Update update, CancellationToken token)
        {
            try
            {
                //switch для обработки приходящих update'ов
                switch (update.Type)
                {
                    //Обработка сообщений
                    case Telegram.Bot.Types.Enums.UpdateType.Message:
                    {
                    
                        
                        var message = update.Message;
                        var user = message.From;
                        var chat = message.Chat;

                        Console.WriteLine($"{DateTime.Now} || {user.Id} || {message.Chat.FirstName} : {message.Text}");
                    
                        //switch для проверки ТИПА
                        switch (message.Type)
                        {
                                
                            //Обработка ТЕКСТА
                            case MessageType.Text:
                                {
                                    //Начало чата (Меню, Корзина, Адрес доставки, Контактная информация)
                                    if (message.Text == "/start")
                                    {
                                        // Кнопки: Меню, Корзина, Адрес доставки, Контактная информация
                                        var replyKeyboard = new ReplyKeyboardMarkup(new[]
                                            {
                                            new[]
                                                {
                                                    new KeyboardButton("Меню")
                                                },
                                            new[]
                                                {
                                                    new KeyboardButton("Корзина")
                                                },
                                            new[]
                                                {
                                                    new KeyboardButton("Адрес доставки")
                                                },
                                            new[]
                                                {
                                                    new KeyboardButton("Контактная информация")
                                                },
                                            })
                                        { ResizeKeyboard = true };

                                        await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Добро пожаловать. \nВыбирите следующий пукт",
                                                            replyMarkup: replyKeyboard);
                                        
                                        return;
                                    }
                                    
                                    //Меню: Пицца, Шаурма, Бургер, Напитки
                                    if (message.Text == "Меню")
                                        {
                                            // Кнопки:Пицца, Шаурма, Бургер, Напитки, Корзина, /start
                                            var replyKeyboard = new ReplyKeyboardMarkup(new[]
                                                {
                                                     new[]
                                                         {
                                                             new KeyboardButton("Пицца"),
                                                             new KeyboardButton("Шаурма_")
                                                         },
                                                     new[]
                                                         {
                                                             new KeyboardButton("Бургеры"),
                                                             new KeyboardButton("Напитки")
                                                         },
                                                     new[]
                                                         {
                                                             new KeyboardButton("Корзина"),
                                                             new KeyboardButton("/start")
                                                         }
                                                })
                                            { ResizeKeyboard = true };

                                            await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Чего изволите? \nВыбирите следующий пукт",
                                                            replyMarkup: replyKeyboard);

                                        }

                                    //Корзина:Оформить заказ, Сбросить, Меню, /start
                                    if (message.Text == "Корзина")
                                        {
                                            //Кнопки: Оформить заказ, Сбросить, Меню, /start
                                            var replyKeyboard = new ReplyKeyboardMarkup(
                                                new []
                                            {
                                                new KeyboardButton[]
                                                {
                                                    new KeyboardButton("Оформить заказ"),
                                                    new KeyboardButton("Сбросить"),
                                                },
                                                new KeyboardButton[]
                                                {
                                                    new KeyboardButton("Меню"),
                                                    new KeyboardButton("/start")
                                                }
                                            })
                                            { ResizeKeyboard = true };

                                            await botClient.SendTextMessageAsync(
                                                           chat.Id,
                                                           "Ваша корзина:\n" + "...\n",
                                                           replyMarkup: replyKeyboard);
                                            
                                        return;
                                    }

                                    //Адрес доставки (кода пока нет)
                                    if(message.Text == "Адрс доставки")
                                        {

                                        }

                                    //Контактная информация
                                    if(message.Text == "Контактная информация")
                                        {
                                            await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Работаем 25/8 без перерыва, \nвыходных, праздников и зарплаты. \nСамовывоз у черта на рогах, \nдоставка сломалась. \nТелефон не работает. \nСпасите))."
                                                                        );
                                        }

                                    //Работа с меню:
                                    //выбор Пицца
                                    if(message.Text == "Пицца")
                                        {
                                            // Кнопки:Пеперони, 4 сыра, Дьябло
                                            var replyKeyboard = new ReplyKeyboardMarkup(new[]
                                                {
                                                     new[]
                                                         {
                                                             new KeyboardButton("Пеперони"),
                                                             new KeyboardButton("4 сыра")
                                                         },
                                                     new[]
                                                         {
                                                             new KeyboardButton("Дьябло"),
                                                             new KeyboardButton("Меню")
                                                         },
                                                     new[]
                                                         {
                                                             new KeyboardButton("Корзина"),
                                                             new KeyboardButton("/start")
                                                         }
                                                })
                                            { ResizeKeyboard = true };

                                            await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Выберите пиццу",
                                                            replyMarkup: replyKeyboard);
                                            return;
                                        }

                                    //выбор Шаурма
                                    if(message.Text == "Шаурма_")
                                        {
                                            // Кнопки:Шаурма, Гирос
                                            var replyKeyboard = new ReplyKeyboardMarkup(new[]
                                                {
                                                     new[]
                                                         {
                                                             new KeyboardButton("Шаурма"),
                                                             new KeyboardButton("Гирос")
                                                         },
                                                     new[]
                                                         {
                                                             new KeyboardButton("Меню")
                                                         },
                                                     new[]
                                                         {
                                                             new KeyboardButton("Корзина"),
                                                             new KeyboardButton("/start")
                                                         }
                                                })
                                            { ResizeKeyboard = true };

                                            await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Выберите шаурму или гирос",
                                                            replyMarkup: replyKeyboard);
                                            return;


                                        }

                                    //Выбор Бургеров
                                    if(message.Text == "Бургеры")
                                        {
                                            // Кнопки:Бургер, Чизбургер
                                            var replyKeyboard = new ReplyKeyboardMarkup(new[]
                                                {
                                                     new[]
                                                         {
                                                             new KeyboardButton("Бургер"),
                                                             new KeyboardButton("Чизбургер")
                                                         },
                                                     new[]
                                                         {
                                                             new KeyboardButton("Меню")
                                                         },
                                                     new[]
                                                         {
                                                             new KeyboardButton("Корзина"),
                                                             new KeyboardButton("/start")
                                                         }
                                                })
                                            { ResizeKeyboard = true };

                                            await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Выберите шаурму или гирос",
                                                            replyMarkup: replyKeyboard);
                                            return;


                                        }
                                    
                                    //Выбор Напитков
                                    if(message.Text == "Напитки")
                                        {
                                            // Кнопки:Капичино, Латте, Еспрессо
                                            var replyKeyboard = new ReplyKeyboardMarkup(new[]
                                                {
                                                     new[]
                                                         {
                                                             new KeyboardButton("Капичино"),
                                                             new KeyboardButton("Латте"),
                                                             new KeyboardButton("Эспрессо")
                                                         },
                                                     new[]
                                                         {
                                                             new KeyboardButton("Корзина"),
                                                             new KeyboardButton("Меню"),
                                                             new KeyboardButton("/start")
                                                         }
                                                })
                                            { ResizeKeyboard = true };

                                            await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Выберите напиток",
                                                            replyMarkup: replyKeyboard);
                                            return;


                                        }


                                    //Пицца
                                    //Выбор Пеперони
                                    if(message.Text == "Пеперони")
                                    {
                                            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                            {
                                                new InlineKeyboardButton[] // создаем массив кнопок
                                                {
                                                    InlineKeyboardButton.WithCallbackData("Большая", "big"),
                                                    InlineKeyboardButton.WithCallbackData("Маленькая", "small"),
                                                },
                                                
                                            });
                                            await using Stream stream = System.IO.File.OpenRead(@"D:\PROJECTS\.NET\Projects\telegram_bot\TG_Shop_delivery\Menu_photo\Pizza\peper.jpg");
                                            await botClient.SendPhotoAsync(chat.Id, InputFile.FromStream(stream, fileName: "Пеперони"));
                                            await botClient.SendTextMessageAsync(
                                                chat.Id,
                                                "Выбирайте",
                                                replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                            return;
                                    }
                                   
                                    //Выбор 4 сыра
                                    if(message.Text == "4 сыра")
                                    {
                                            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                            {
                                                new InlineKeyboardButton[] // создаем массив кнопок
                                                {
                                                    InlineKeyboardButton.WithCallbackData("Большая", "big"),
                                                    InlineKeyboardButton.WithCallbackData("Маленькая", "small"),
                                                },
                                                
                                            });
                                            await using Stream stream = System.IO.File.OpenRead(@"D:\PROJECTS\.NET\Projects\telegram_bot\TG_Shop_delivery\Menu_photo\Pizza\4cheese.jpg");
                                            await botClient.SendPhotoAsync(chat.Id, InputFile.FromStream(stream, fileName: "4 сыра"));
                                            await botClient.SendTextMessageAsync(
                                                chat.Id,
                                                "Выбирайте",
                                                replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                            return;
                                    }

                                    //Выбор Дьябло
                                    if(message.Text == "Дьябло")
                                    {
                                            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                            {
                                                new InlineKeyboardButton[] // создаем массив кнопок
                                                {
                                                    InlineKeyboardButton.WithCallbackData("Большая", "big"),
                                                    InlineKeyboardButton.WithCallbackData("Маленькая", "small"),
                                                },
                                                
                                            });
                                            await using Stream stream = System.IO.File.OpenRead(@"D:\PROJECTS\.NET\Projects\telegram_bot\TG_Shop_delivery\Menu_photo\Pizza\devil.jpg");
                                            await botClient.SendPhotoAsync(chat.Id, InputFile.FromStream(stream, fileName: "4 сыра"));
                                            await botClient.SendTextMessageAsync(
                                                chat.Id,
                                                "Выбирайте",
                                                replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                            return;
                                    }


                                    //Шаурма
                                    //Выбор Шаурма
                                    if (message.Text == "Шаурма")
                                    {
                                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                        {
                                            new InlineKeyboardButton[] // создаем массив кнопок
                                            {
                                                InlineKeyboardButton.WithCallbackData("Большая", "big"),
                                                InlineKeyboardButton.WithCallbackData("Маленькая", "small"),
                                            },
                                            
                                        });
                                        await using Stream stream = System.IO.File.OpenRead(@"D:\PROJECTS\.NET\Projects\telegram_bot\TG_Shop_delivery\Menu_photo\Shaurma\shaurma.jpg");
                                        await botClient.SendPhotoAsync(chat.Id, InputFile.FromStream(stream, fileName: "Шаурма"));
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Выбирайте",
                                            replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }

                                    //Выбор Гирос
                                    if (message.Text == "Гирос")
                                    {
                                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                        {
                                            new InlineKeyboardButton[] // создаем массив кнопок
                                            {
                                                InlineKeyboardButton.WithCallbackData("Большой", "big"),
                                                InlineKeyboardButton.WithCallbackData("Маленький", "small"),
                                            },
                                            
                                        });
                                        await using Stream stream = System.IO.File.OpenRead(@"D:\PROJECTS\.NET\Projects\telegram_bot\TG_Shop_delivery\Menu_photo\Shaurma\giros.jpg");
                                        await botClient.SendPhotoAsync(chat.Id, InputFile.FromStream(stream, fileName: "Гирос"));
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Выбирайте",
                                            replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }


                                    //Бургеры
                                    //Выбор Бургер
                                    if (message.Text == "Бургер")
                                    {
                                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                        {
                                            new InlineKeyboardButton[] // создаем массив кнопок
                                            {
                                                InlineKeyboardButton.WithCallbackData("Большой", "big"),
                                                InlineKeyboardButton.WithCallbackData("Маленький", "small"),
                                            },
                                            
                                        });
                                        await using Stream stream = System.IO.File.OpenRead(@"D:\PROJECTS\.NET\Projects\telegram_bot\TG_Shop_delivery\Menu_photo\Burger\Burg.jpg");
                                        await botClient.SendPhotoAsync(chat.Id, InputFile.FromStream(stream, fileName: "Бургер"));
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Выбирайте",
                                            replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }

                                    //Выбор Чизбургер
                                    if (message.Text == "Чизбургер")
                                    {
                                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                        {
                                            new InlineKeyboardButton[] // создаем массив кнопок
                                            {
                                                InlineKeyboardButton.WithCallbackData("Большой", "big"),
                                                InlineKeyboardButton.WithCallbackData("Маленький", "small"),
                                            },
                                            
                                        });
                                        await using Stream stream = System.IO.File.OpenRead(@"D:\PROJECTS\.NET\Projects\telegram_bot\TG_Shop_delivery\Menu_photo\Burger\Cheese-bur.jpg");
                                        await botClient.SendPhotoAsync(chat.Id, InputFile.FromStream(stream, fileName: "Чизбургер"));
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Выбирайте",
                                            replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }


                                    //Напитки
                                    //Выбор Капичино
                                    if (message.Text == "Капичино")
                                    {
                                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                        {
                                            new InlineKeyboardButton[] // создаем массив кнопок
                                            {
                                                InlineKeyboardButton.WithCallbackData("Большой", "big"),
                                                InlineKeyboardButton.WithCallbackData("Маленький", "small"),
                                            },
                                            
                                        });
                                        await using Stream stream = System.IO.File.OpenRead(@"D:\PROJECTS\.NET\Projects\telegram_bot\TG_Shop_delivery\Menu_photo\Drink\capp.jpg");
                                        await botClient.SendPhotoAsync(chat.Id, InputFile.FromStream(stream, fileName: "Капичино"));
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Выбирайте",
                                            replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }

                                    //Выбор Латте
                                    if (message.Text == "Латте")
                                    {
                                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                        {
                                            new InlineKeyboardButton[] // создаем массив кнопок
                                            {
                                                InlineKeyboardButton.WithCallbackData("Большой", "big"),
                                                InlineKeyboardButton.WithCallbackData("Маленький", "small"),
                                            },
                                            
                                        });
                                        await using Stream stream = System.IO.File.OpenRead(@"D:\PROJECTS\.NET\Projects\telegram_bot\TG_Shop_delivery\Menu_photo\Drink\latte.jpg");
                                        await botClient.SendPhotoAsync(chat.Id, InputFile.FromStream(stream, fileName: "Латте"));
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Выбирайте",
                                            replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }

                                    //Выбор Эспрессо
                                    if (message.Text == "Эспрессо")
                                    {
                                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                        {
                                            new InlineKeyboardButton[] // создаем массив кнопок
                                            {
                                                InlineKeyboardButton.WithCallbackData("Большой", "big"),
                                                InlineKeyboardButton.WithCallbackData("Маленький", "small"),
                                            },
                                            
                                        });
                                        await using Stream stream = System.IO.File.OpenRead(@"D:\PROJECTS\.NET\Projects\telegram_bot\TG_Shop_delivery\Menu_photo\Drink\espr.jpg");
                                        await botClient.SendPhotoAsync(chat.Id, InputFile.FromStream(stream, fileName: "Эспрессо"));
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Выбирайте",
                                            replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                        return;
                                    }


                                        


                    
                                    return;
                                }
                    
                        };
                        break;
                    }

                    //Обработка iline кнопок
                    case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    {
                            var callbackQuery = update.CallbackQuery;
                            var user = callbackQuery.From;
                            var chat = callbackQuery.Message.Chat;
                            var message = update.Message;

                            switch (callbackQuery.Data)
                            {
                                case "big":
                                    {                                      
                                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                    await botClient.SendTextMessageAsync(
                                                        chat.Id,
                                                        $"Выбрано: , размер {callbackQuery.Data}");
                                        return;
                                    }
                                case "small": 
                                    {
                                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                        await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            $"Выбрано: , размер {callbackQuery.Data}");
                                        return;

                                    }

                                return;
                            }


                            return;
                    }
                        


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
