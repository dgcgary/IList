using System;
using System.Collections.Generic;

/// Enum para definir la dirección de ordenamiento.
public enum SortDirection
{
    Ascending,
    Descending
}


/// Interfaz que define las operaciones básicas para una lista.
public interface IList
{
    void InsertInOrder(int value);
    int DeleteFirst();
    int DeleteLast();
    bool DeleteValue(int value);
    int GetMiddle();
    void MergeSorted(IList listA, IList listB, SortDirection direction);
}

/// Clase que representa un nodo en una lista doblemente enlazada.
public class Node
{
    public int Value { get; set; }
    public Node? Next { get; set; }
    public Node? Previous { get; set; }

    public Node(int value)
    {
        this.Value = value;
        this.Next = null;
        this.Previous = null;
    }
}


/// Clase que representa una lista doblemente enlazada.
public class DoublyLinkedList : IList
{
    public Node? head { get; set; }
    public Node? tail { get; set; }
    private Node? middle;
    private int count;

    public DoublyLinkedList()
    {
        this.head = null;
        this.tail = null;
        this.middle = null;
        this.count = 0;
    }

    /// Inserta un valor en la lista manteniendo el orden.
    public void InsertInOrder(int value)
    {
        Node newNode = new Node(value);

        if (this.head == null)
        {
            // La lista está vacía, el nuevo nodo se convierte en la cabeza, la cola y el medio
            this.head = newNode;
            this.tail = newNode;
            this.middle = newNode;
            this.count = 1;
            return;
        }

        Node current = this.head;

        // Si el nuevo valor es menor que la cabeza, se inserta al principio
        if (value <= current.Value)
        {
            newNode.Next = this.head;
            this.head.Previous = newNode;
            this.head = newNode;
        }
        else
        {
            // Buscar la posición correcta para insertar el nuevo nodo
            while (current.Next != null && current.Next.Value < value)
            {
                current = current.Next;
            }

            // Insertar el nuevo nodo en la posición encontrada
            newNode.Next = current.Next;
            if (current.Next != null)
            {
                current.Next.Previous = newNode;
            }
            else
            {
                // Si se inserta al final, actualizar la cola
                this.tail = newNode;
            }
            current.Next = newNode;
            newNode.Previous = current;
        }

        this.count++;

        // Actualizar el nodo medio
        if (this.count % 2 == 0)
        {
            if (value >= this.middle!.Value)
            {
                this.middle = this.middle.Next;
            }
        }
        else
        {
            if (value < this.middle!.Value)
            {
                this.middle = this.middle.Previous;
            }
        }
    }

    /// Obtiene el valor del nodo medio de la lista.
    public int GetMiddle()
    {
        if (this.middle == null)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }
        return this.middle.Value;
    }


    /// Elimina y retorna el primer valor de la lista.
    public int DeleteFirst()
    {
        if (this.head == null)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }

        int value = this.head.Value;
        this.head = this.head.Next;

        if (this.head != null)
        {
            this.head.Previous = null;
        }
        else
        {
            this.tail = null;
        }

        return value;
    }

    /// Elimina y retorna el último valor de la lista.
    public int DeleteLast()
    {
        if (tail == null) throw new InvalidOperationException("La lista está vacía.");
        int value = tail.Value;
        tail = tail.Previous;
        if (tail != null)
        {
            tail.Next = null;
        }
        else
        {
            head = null;
        }
        return value;
    }

    /// Elimina el primer nodo que contiene el valor especificado.
    public bool DeleteValue(int value)
    {
        Node? current = head;
        while (current != null && current.Value != value)
        {
            current = current.Next;
        }
        if (current == null) return false;
        if (current == head)
        {
            head = head.Next;
            if (head != null)
            {
                head.Previous = null;
            }
            else
            {
                tail = null;
            }
        }
        else if (current == tail)
        {
            tail = tail.Previous;
            if (tail != null)
            {
                tail.Next = null;
            }
            else
            {
                head = null;
            }
        }
        else
        {
            current.Previous!.Next = current.Next;
            current.Next!.Previous = current.Previous;
        }
        return true;
    }

    /// Fusiona una listA y una listB y devuelve la listA con la modificacion respectiva
    public void MergeSorted(IList listA, IList listB, SortDirection direction)
    {
        if (listA == null)
        {
            throw new ArgumentNullException(nameof(listA), "La lista A no puede ser nula.");
        }

        if (listB == null)
        {
            throw new ArgumentNullException(nameof(listB), "La lista B no puede ser nula.");
        }

        if (!(listA is DoublyLinkedList) || !(listB is DoublyLinkedList))
        {
            throw new ArgumentException("Las listas deben ser de tipo DoublyLinkedList.");
        }

        var listADoubly = (DoublyLinkedList)listA;
        var listBDoubly = (DoublyLinkedList)listB;

        Node? currentA = listADoubly.head;
        Node? currentB = listBDoubly.head;
        Node? prevA = null;

        while (currentA != null && currentB != null)
        {
            if (currentA.Value <= currentB.Value)
            {
                prevA = currentA;
                currentA = currentA.Next;
            }
            else
            {
                Node? nextB = currentB.Next;

                if (prevA != null)
                {
                    prevA.Next = currentB;
                    currentB.Previous = prevA;
                }
                else
                {
                    listADoubly.head = currentB;
                    currentB.Previous = null;
                }

                currentB.Next = currentA;
                if (currentA != null)
                {
                    currentA.Previous = currentB;
                }

                prevA = currentB;
                currentB = nextB;
            }
        }

        if (currentB != null)
        {
            if (prevA != null)
            {
                prevA.Next = currentB;
                currentB.Previous = prevA;
            }
            else
            {
                listADoubly.head = currentB;
                currentB.Previous = null;
            }
        }

        // Actualizar la cola de listA
        while (listADoubly.tail != null && listADoubly.tail.Next != null)
        {
            listADoubly.tail = listADoubly.tail.Next;
        }

        // Si la dirección es descendente, invertir la lista
        if (direction == SortDirection.Descending)
        {
            listADoubly.InvertirLista();
        }
    }


    /// Invierte el orden de los nodos en la lista.

    public void InvertirLista()
    {
        if (this.head == null)
        {
            return; // Lista vacía, no hay nada que invertir
        }

        Node? current = this.head;
        Node? temp = null;

        // Intercambiar los punteros siguiente y anterior para todos los nodos
        while (current != null)
        {
            temp = current.Previous;
            current.Previous = current.Next;
            current.Next = temp;
            current = current.Previous;
        }

        // Ajustar la cabeza y la cola
        if (temp != null)
        {
            this.head = temp.Previous;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
          
        }
    }
}
