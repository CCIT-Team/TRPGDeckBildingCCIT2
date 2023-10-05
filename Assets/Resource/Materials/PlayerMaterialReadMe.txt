기본적으로 ColorPalette.png 텍스쳐 파일을 사용한 머테리얼들은
64*64 텍스쳐이기때문에 0.015625씩 offset 조정을 하면 색이 한단계씩 변경됩니다.

PlayerSkinMaterial//
머테리얼 offset의 y 값을 -0.015625씩 조정하면 피부색이 바뀝니다.
ex) offset(0, 0) = new Vector2(0, -(0.015625*skinColor))

offset (0,0) : 휴먼 피부색 1번, skinColor : 0
휴먼 피부색 2번, skinColor : 1
휴먼 피부색 3번, skinColor : 2
휴먼 피부색 4번, skinColor : 3
엘프 피부색 1번, skinColor : 4
엘프 피부색 2번, skinColor : 5
엘프 피부색 3번, skinColor : 6
드로우 피부색 1번, skinColor : 7
드로우 피부색 2번, skinColor : 8
드로우 피부색 3번, skinColor : 9
드로우 피부색 4번, skinColor : 10
하프오크 피부색 1번, skinColor : 11
하프오크 피부색 2번, skinColor : 12
하프오크 피부색 3번, skinColor : 13
하프오크 피부색 4번, skinColor : 14


PlayerEyeMaterial//
플레이어가 정할 수 있는 눈동자 색의 갯수는 13개로
위와 동일하게 y축으로 -0.015625씩 조정하면 눈동자색이 바뀝니다.
ex) offset(0, 0) = new Vector2(0, -(0.015625*eyeColor))

offset(0, 0) : 눈동자 1번 eyeColor:0 ~ 눈동자 13번 eyeColor:12