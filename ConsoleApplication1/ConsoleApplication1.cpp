// ConsoleApplication1.cpp : 이 파일에는 'main' 함수가 포함됩니다. 거기서 프로그램 실행이 시작되고 종료됩니다.
//

#include <iostream>

int main()
{
    string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        int L = int.Parse(inputs[1]); // the number of links
        int E = int.Parse(inputs[2]); // the number of exit gateways
        int[,] Links = new int[N,N];
        int[] Ends = new int[E];
        
        Console.Error.WriteLine($"N : {N}"); 
        Console.Error.WriteLine($"L : {L}"); 
        Console.Error.WriteLine($"E : {E}"); 
        for (int i = 0; i < L; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            int N2 = int.Parse(inputs[1]);
            Console.Error.WriteLine($"N1 : {N1}"); 
            Console.Error.WriteLine($"N2 : {N2}");
            Links[N1,N2] = 1;
            Links[N2,N1] = 1;
        }
        for (int i = 0; i < E; i++)
        {
            int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
            Console.Error.WriteLine($"EI : {EI}");
            Ends[i] = EI;
        }

        // game loop
        while (true)
        {
            Queue<int> que = new Queue<int>();
            bool[] c = new bool[N];
            int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn
            que.Enqueue(SI);
            c[SI] = true;
            Console.Error.WriteLine($"SI : {SI}");
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // Example: 0 1 are the indices of the nodes you wish to sever the link between
            
            bool flag = false;
            while (que.Count == 0)
            {
                var v = que.Dequeue();
                for (int i = 0; i < N; i++)
                {
                    if (Links[v][i] == 1 && !c[i])
                    {
                        que.Enqueue(i);
                        c[i] = true;
                        foreach (int end in Ends)
                        {
                            if (v == end)
                            {
                                flag = true;
                                Links[v][i] = 0;
                                Links[i][v] = 0;
                                Console.WriteLine($"{v} {end}");
                                break;
                            }
                        }
                        if (flag)
                            break;
                    }
                    if (flag)
                            break;
                }
            }
            
        }
    }
}

// 프로그램 실행: <Ctrl+F5> 또는 [디버그] > [디버깅하지 않고 시작] 메뉴
// 프로그램 디버그: <F5> 키 또는 [디버그] > [디버깅 시작] 메뉴

// 시작을 위한 팁: 
//   1. [솔루션 탐색기] 창을 사용하여 파일을 추가/관리합니다.
//   2. [팀 탐색기] 창을 사용하여 소스 제어에 연결합니다.
//   3. [출력] 창을 사용하여 빌드 출력 및 기타 메시지를 확인합니다.
//   4. [오류 목록] 창을 사용하여 오류를 봅니다.
//   5. [프로젝트] > [새 항목 추가]로 이동하여 새 코드 파일을 만들거나, [프로젝트] > [기존 항목 추가]로 이동하여 기존 코드 파일을 프로젝트에 추가합니다.
//   6. 나중에 이 프로젝트를 다시 열려면 [파일] > [열기] > [프로젝트]로 이동하고 .sln 파일을 선택합니다.
