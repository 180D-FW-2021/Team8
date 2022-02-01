import HandsWrapper as hands

print('start\n')
#hands.hands_single(frame_wait = 13) # 13 seems to be an optimal value
hands.hands_tcp(frame_wait = 5)
