b = bluetooth('Bluetooth-Master');
b = bluetooth('HC-05');
fopen(b);
fwrite(b,[0xAA,0xBB,0,0,0,0,1,0xCC]);

%% 示例程序

%初始化于机械手的通信,只能初始化一次,请勿多次初始化,若需要多次初始化请加异常捕获
b = hand_init('HC-05');

%其他程序
%...
%需要控制机械时
hand_control(0,0,0,0,0);