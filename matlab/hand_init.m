function [b] = hand_init(device_name)
%HAND_INIT 初始化蓝牙，建立连接
%   device_name 蓝牙设备名称
%   蓝牙设备名称，默认可能为'Bluetooth-Master' 或 'HC-05'
b = bluetooth(device_name);
fopen(b);
end
