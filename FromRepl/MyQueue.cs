using System;

namespace RepDLL
{
    public class MyQueue
    {
        // Массив с элементами
        private string[] _array;
        // Индекс начального элемента.
        private int _head;
        // Индекс конечного элемента.
        private int _tail;

        /// <summary>
        /// Создаём очередь. Начальная ёмкость - 4;
        /// </summary>
        public MyQueue()
        {
            _array = new string[4];
        }

        /// <summary>
        /// Количество элементов в очереди.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Очистка очереди.
        /// </summary>
        public void Clear()
        {
            // Очищаем все поля.
            _array = new string[4];
            _head = 0;
            _tail = 0;
            Count = 0;
        }

        /// <summary>
        /// Извлечение элемента из очереди.
        /// </summary>
        /// <returns>Извлечённый элемент.</returns>
        public string Dequeue()
        {
            // Проверяем, можно ли что-либо достать из очереди.
            if (Count == 0)
                throw new InvalidOperationException();
            // Достаём первый элемент.
            string local = _array[_head];
            // Обнуляем первый элемент.
            _array[_head] = "";
            // Изменяем индекс начала элементов в массиве.
            _head = (_head + 1) % _array.Length;
            // Убавляем количество элементов.
            Count--;
            return local;
        }

        /// <summary>
        /// Добавление элемента в очередь.
        /// </summary>
        /// <param name="item">Добавляемый элемент.</param>
        public void Enqueue(string item)
        {
            // Проверяем ёмкость массива, если недостаточна - удваиваем.
            if (Count == _array.Length)
            {
                var capacity = _array.Length * 2;
                SetCapacity(capacity);
            }
            // Устанавливаем последний элемент.
            _array[_tail] = item;
            // Изменяем индекс конца массива.
            _tail = (_tail + 1) % _array.Length;
            // Прибавляем количество элементов.
            Count++;
        }


        /// <summary>
        /// Просмотр элемента на вершине очереди.
        /// </summary>
        /// <returns>Верхний элемент.</returns>
        public string Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            // Возвращаем верхний элемент без его удаления.
            return _array[_head];
        }

        // Изменение ёмкости очереди.
        private void SetCapacity(int capacity)
        {
            // Новый массив заданного объёма.
            string[] destinationArray = new string[capacity];
            if (Count > 0)
            {
                // Копируем старый массив в новый.
                if (_head < _tail)
                    Array.Copy(_array, _head, destinationArray, 0, Count);
                else
                {
                    Array.Copy(_array, _head, destinationArray, 0, _array.Length - _head);
                    Array.Copy(_array, 0, destinationArray, _array.Length - _head, _tail);
                }
            }
            _array = destinationArray;
            // Новые значения индексов начала и конца массива.
            _head = 0;
            if (Count == capacity)
                _tail = 0;
            else
                _tail = Count;
        }
    }
}
