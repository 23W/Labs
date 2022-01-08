#define queueLength 2

mtype = {mng1, mng2};

chan data = [queueLength] of {mtype, int, int};
chan result = [queueLength] of {mtype, int};

active proctype Euclid()
{
    mtype managerId;
    int x = 0, y = 0;

    do
    :: data ? managerId, x, y -> assert(x!=0 && y!=0);
                                 do 
                                 :: (x > y) -> x = x - y 
                                 :: (x < y) -> y = y - x 
                                 :: (x == y) -> result ! managerId, x; break;
                                 od
    od
}

active proctype Manager1()
{
    int dX[3] = {90, 90, 5};
    int dY[3] = {70, 150, 3};
    int dA[3] = {10, 30, 1};

    int answer = 0;
    int variant = 0;
    int state = 0;

    do
    :: (state==0) -> variant = 0; state = 1;
    :: (state==0) -> variant = 1; state = 1;
    :: (state==0) -> variant = 2; state = 1;
    :: (state==1) -> data ! mng1, dX[variant], dY[variant];
                     result ? mng1, answer;
                     assert(answer == dA[variant]);
                     state = 0;
    od
}

active proctype Manager2()
{
    int x = 0;
    int y = 0;
    int answer = 0;
    int state = 0;

    do
    :: (state==0) -> select (x: 1 .. 50); 
                     select (y: 1 .. 50);
                     state = 1;
    :: (state==1) -> data ! mng2, x, y;
                     result ? mng2, answer;
                     assert(answer <=x && answer <=y &&
                            x % answer == 0 && y % answer == 0);
                     state = 0;
    od
}

ltl check_gcd { []((Euclid:x!=Euclid:y) -> <>(Euclid:x==Euclid:y)) }