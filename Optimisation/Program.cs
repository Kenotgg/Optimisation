using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Optimisation
{
    class Program
    {
        
        //Код который выполняет подсчёт частоты слов в строке.
        static void Main(string[] args)
        {
            //Вносим исходный текст
            string text = "Это пример текста. Текст должен быть проанализирован. Это важно.";
            //Закидываем в метод WordFrequency(text).
            WordFrequency(text);
            Console.ReadKey();
        }

        static void WordFrequency(string text)
        {
            //Преобразуем в строчный текст(Заглавные буквы становятся строчными).
            text = text.ToLower();
            //Используем Regex для того чтобы правильно отделить слово убрав лишние знаки.
            //Рассмотрим подробнее:
            //1) Regex.Matches — это метод класса Regex, который возвращает коллекцию всех совпадений.
            //2) text - входная строка в которой осуществляется поиск.
            //3) @"\b\w+\b" - регулярное выражение - находит все целые слова состоящие из букв или цифр.
            //4) OfType<Match>(): возвращает коллекцию MatchCollection, содржащую объекты Match, преобразует эту коллекцию в последовательность только объектов Match.
            //5) Select(m => m.Value): Это метод LINQ, который преобразует каждую Match в её строковое значение,
            //5.1) m это переменная, представляющая текущий объект Match.
            //5.2) m.Value -  это строка, которая соответствует найденному совпадению.
            //6) ToArray() - метод преобразует последовательность строк в массив строк string[].
            string[] words = Regex.Matches(text, @"\b\w+\b").OfType<Match>().Select(m => m.Value).ToArray(); 
            //Инициализируем словарь.
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            //Проходимся по тексту и проверяем условие на повторение слова.
            foreach(string word in words)
            {
                //если слово найдено в wordCounts
                if(wordCounts.TryGetValue(word, out int count)) //count - выходной параметр, если слово найдено в словаре, то его частота записывается в count.
                {
                    wordCounts[word] = ++count;  //увеличиваем count без дополнительного обращения к словарю как в случае wordCounts[word] = count + 1;     
                }
                else //если слово не найдено в wordCounts, то пишем, что найдено только один раз.
                {
                    wordCounts[word] = 1; 
                }
            }
           //Сортируем словарь по ключу в порядке его убывания, здесь же выводим результат в консоль.
            foreach (var item in wordCounts.OrderByDescending(x => x.Value))
            {
                Console.WriteLine(item.Key + ": " + item.Value); 
            }

        }
    }
}
