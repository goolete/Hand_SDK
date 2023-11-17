function hand_control(b,finger1,finger2,finger3,finger4,finger5)
%HAND_CONTROL 控制机械手状态
%   finger1-5分别对应手指1-5伸出/收回的状态
%   0-手指收回     1-手指伸出
fwrite(b,[0xAA,0xBB,finger1,finger2,finger3,finger4,finger5,0xCC]);
end

