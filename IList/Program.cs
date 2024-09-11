using System;
using System.Collections.Generic;

public enum SortDirection
{
    Ascending,
    Descending
}

public interface IList
{
    void InsertInOrder(int value);
    int DeleteFirst();
    int DeleteLast();
    bool DeleteValue(int value);
    int GetMiddle();
    void MergeSorted(IList listA, IList listB, SortDirection direction);
}

public class Node
{
    public int Value { get; set; }
    public Node Next { get; set; }
    public Node Previous { get; set; }

    public Node(int value)
    {
        Value = value;
        Next = null;
        Previous = null;
    }
}

public class DoublyLinkedList : IList
{
    private Node head;
    private Node tail;

    public DoublyLinkedList()
    {
        head = null;
        tail = null;
    }

    public void InsertInOrder(int value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            Node current = head;
            while (current != null && current.Value < value)
            {
                current = current.Next;
            }
            if (current == head)
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            else if (current == null)
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
            else
            {
                newNode.Next = current;
                newNode.Previous = current.Previous;
                current.Previous.Next = newNode;
                current.Previous = newNode;
            }
        }
    }

    public int DeleteFirst()
    {
        if (head == null) throw new InvalidOperationException("List is empty");
        int value = head.Value;
        head = head.Next;
        if (head != null)
        {
            head.Previous = null;
        }
        else
        {
            tail = null;
        }
        return value;
    }

    public int DeleteLast()
    {
        if (tail == null) throw new InvalidOperationException("List is empty");
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

    public bool DeleteValue(int value)
    {
        Node current = head;
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
            current.Previous.Next = current.Next;
            current.Next.Previous = current.Previous;
        }
        return true;
    }

    public int GetMiddle()
    {
        if (head == null) throw new InvalidOperationException("List is empty");
        Node slow = head;
        Node fast = head;
        while (fast != null && fast.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;
        }
        return slow.Value;
    }

    public void MergeSorted(IList listA, IList listB, SortDirection direction)
    {
        if (listA == null || listB == null)
        {
            throw new ArgumentNullException("Both lists must be non-null");
        }

        DoublyLinkedList mergedList = new DoublyLinkedList();
        Node currentA = ((DoublyLinkedList)listA).head;
        Node currentB = ((DoublyLinkedList)listB).head;

        while (currentA != null && currentB != null)
        {
            if ((direction == SortDirection.Ascending && currentA.Value <= currentB.Value) ||
                (direction == SortDirection.Descending && currentA.Value >= currentB.Value))
            {
                mergedList.InsertInOrder(currentA.Value);
                currentA = currentA.Next;
            }
            else
            {
                mergedList.InsertInOrder(currentB.Value);
                currentB = currentB.Next;
            }
        }

        while (currentA != null)
        {
            mergedList.InsertInOrder(currentA.Value);
            currentA = currentA.Next;
        }

        while (currentB != null)
        {
            mergedList.InsertInOrder(currentB.Value);
            currentB = currentB.Next;
        }

        if (direction == SortDirection.Descending)
        {
            // Invertir la lista para el orden descendente
            Node prev = null;
            Node current = mergedList.head;
            Node next = null;
            while (current != null)
            {
                next = current.Next;
                current.Next = prev;
                current.Previous = next;
                prev = current;
                current = next;
            }
            mergedList.head = prev;
        }

        this.head = mergedList.head;
        this.tail = mergedList.tail;
    }

}

class Program
{
    static void Main(string[] args)
    {
        // Ejemplo de uso de la lista doblemente enlazada
        DoublyLinkedList list = new DoublyLinkedList();
        list.InsertInOrder(3);
        list.InsertInOrder(1);
        list.InsertInOrder(2);

        Console.WriteLine("Lista después de insertar 1, 2, 3 en orden:");
        while (true)
        {
            try
            {
                Console.WriteLine(list.DeleteFirst());
            }
            catch (InvalidOperationException)
            {
                break;
            }
        }
    }
}
