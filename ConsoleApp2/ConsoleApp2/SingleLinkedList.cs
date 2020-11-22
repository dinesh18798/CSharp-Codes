using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class SingleLinkedList
    {
        class SinglyLinkedListNode
        {
            public int data;
            public SinglyLinkedListNode next;
        }

        static SinglyLinkedListNode reverse(SinglyLinkedListNode head)
        {
            SinglyLinkedListNode currentHead = head;
            SinglyLinkedListNode next = null;
            SinglyLinkedListNode prev = null;

            while (currentHead != null)
            {
                next = currentHead.next;
                currentHead.next = prev;
                prev = currentHead;
                currentHead = next;
            }
            return prev;
        }

    }
}
