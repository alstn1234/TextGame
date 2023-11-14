# TextGame
장비를 구하고 던전을 탐험하는 Text RPG게임

# 개발 기간
23.11.08 ~ 23.11.11

# 구현 기능
- 1. 게임 시작 화면
    - 게임 시작시 간단한 소개 말과 마을에서 할 수 있는 행동을 알려줍니다.
    - 원하는 행동의 숫자를 타이핑하면 실행합니다. 
    1 ~ 2 이외 입력시 - **잘못된 입력입니다** 출력
      ![image](https://github.com/alstn1234/TextGame/assets/102529677/f094c940-ffa2-4ea5-bf65-13bcfd3aba43)




- 2. 상태보기
    - 캐릭터의 정보를 표시합니다.
    - 7개의 속성을 가지고 있습니다.
    레벨 / 이름 / 직업 / 공격력 / 방어력 / 체력 / Gold
    - 처음 기본값은 이름을 제외하고는 아래와 동일하게 만들어주세요
    - 이후 장착한 아이템에 따라 수치가 변경 될 수 있습니다.
      ![image](https://github.com/alstn1234/TextGame/assets/102529677/8e3a0f09-2e43-4d85-ab94-19712c2f70f6)



    
- 3. 인벤토리
    - 보유 중인 아이템을 전부 보여줍니다.
    이때 장착중인 아이템 앞에는 [E] 표시를 붙여 줍니다.
     - 인벤토리 내용을 보면 이름의 글자 갯수, 설명의 글자 갯수가 각각 다릅니다.
      - 이름에 따라 크기가 결정되지 않고 인벤토리 레이아웃을 고정
    ![image](https://github.com/alstn1234/TextGame/assets/102529677/acefce4b-66f3-4470-a518-cd4747b2e138)




  - 3 - 1. 장착 관리 
  - 장착관리가 시작되면 아이템 목록 앞에 숫자가 표시됩니다.
  - 각 타입별로 하나의 아이템만 장착가능 - ( 방어구 / 무기 )
  - 방어구를 장착하면 기존 방어구가 있다면 해제하고 장착
  - 무기를 장착하면 기존 무기가 있다면 해제하고 장착
  - 일치하는 아이템을 선택했다면 (예제에서 1~2선택시)
      - 장착중이지 않다면 → 장착
    [E] 표시 추가
    - 이미 장착중이라면 → 장착 해제
    [E] 표시 없애기
  - 일치하는 아이템을 선택했지 않았다면 (예제에서 1~3이외 선택시)
      - **잘못된 입력입니다** 출력
    ![image](https://github.com/alstn1234/TextGame/assets/102529677/1f71a94c-4fbc-40e9-80d0-1bb812c2f75e)


- 3 - 2. 인벤토리 정렬하기  (난이도 - ★★★☆☆)
    - 인벤토리에 있는 아이템을 이름순, 장착순, 공격력순, 방어력순으로 정렬
   ![image](https://github.com/alstn1234/TextGame/assets/102529677/93920456-07c1-4803-8636-efbbdb0539be)
      
  
- 4. 상점 - 아이템 구매 (난이도 - ★★★★☆)
    - 게임 시작 화면에 3. 상점을 추가합니다.
    - 보유중인 골드와 아이템의 정보,가격이 표시됩니다.
    - 아이템 정보 오른쪽에는 가격이 표시가 됩니다.
    - 이미 구매를 완료한 아이템이라면 **구매완료** 로 표시됩니다.
    - **아이템 구매** 를 선택하면 아이템 목록 앞에 숫자가 표시됩니다.
    ![image](https://github.com/alstn1234/TextGame/assets/102529677/c418dd65-cb6f-4f94-bc57-317d1debaf7d)

   
    
    - 일치하는 아이템을 선택했다면 (예제에서 0~6선택시)
        - 이미 구매한 아이템이라면
        → **이미 구매한 아이템입니다** 출력
        - 구매가 가능하다면
            - 보유 금액이 충분하다면
            → **구매를 완료했습니다.** 출력
            재화 감소 / 인벤토리에 아이템 추가 / 상점에 구매완료 표시
            - 보유 금액이 부족하다면
            → **Gold 가 부족합니다.** 출력
    - 일치하는 아이템을 선택했지 않았다면 (예제에서 1~3이외 선택시)
        - **잘못된 입력입니다** 출력
      ![image](https://github.com/alstn1234/TextGame/assets/102529677/992cb8c8-fc47-49d7-9cfa-ed33e06dd526)

    
- 4 - 1. 상점 - 아이템 판매 (난이도 - ★★★☆☆) - 선행과제 [7. 상점 - 아이템 구매]
    - 상점에 아이템 판매 기능을 추가합니다.
    - 판매 시 구매가격의 **85% 가격**에 판매합니다.
    - 판매시 **장착하고 있는 아이템이었다면 해제** 됩니다.
      ![image](https://github.com/alstn1234/TextGame/assets/102529677/de0c4492-9ca2-4498-a09b-6c6b0fa291b7)
     
    
- 5. 던전입장  (난이도 - ★★★★☆)
    - 던전은 3가지 난이도가 있습니다.
    - 방어력으로 던전을 수행할 수 있을지 판단합니다.
        - 권장 방어력보다 낮다면
            - 40% 확률로 던전 실패
                - 보상 없고 체력 감소 절반
        - 권장 방어력 보다 높다면
            - 던전 클리어
                - 권장 방어력 +- 에 따라 종료시 체력 소모 반영
                    - 기본 체력 감소량
                        - 20 ~ 35 랜덤
                        - (내 방어력 - 권장 방어력) 만큼 랜덤 값에 설정
                        - ex) 권장 방어력 5, 내 방어력 7
                        20(-2) ~ 35(-2) 랜덤
                        - ex) 권장 방어력 11, 내 방어력 5
                        20(+6) ~ 35(+6) 랜덤
    - 공격력으로 던전 클리어시 보상을 어느정도 얻을지 계산됩니다.
        - 각 던전별 기본 클리어 보상
            - 쉬운 던전 - 1000 G
            - 일반 던전 - 1700 G
            - 어려운 던전 - 2500 G
        - 공격력  ~ 공격력 * 2 의 % 만큼 추가 보상 획득 가능
            - ex) 공격력 10, 쉬움 던전
            기본 보상 1000 G
            공격력으로 인한 추가 보상 10 ~ 20%
            - ex) 공격력 15, 어려운 던전
            기본 보상 2500 G
            공격력으로 인한 추가 보상 15 ~ 30%
    ![image](https://github.com/alstn1234/TextGame/assets/102529677/3bc1fa72-cf56-43b8-a3b1-26c516453c08)
    
    ![image](https://github.com/alstn1234/TextGame/assets/102529677/a278b64e-4cc6-4203-905b-eda5cf3d4c58)


- 5 - 1. 휴식 기능  (난이도 - ★☆☆☆☆)  - 선행 과제 [9. 던전입장]
    - 휴식을 선택하면 500G 으로 랜덤한 체력을 회복합니다.
        - 보유 금액이 충분하다면
        → **휴식을 완료했습니다.** 출력
        - 보유 금액이 부족하다면
        → **Gold 가 부족합니다.** 출력
    - 휴식시 체력은 100까지 회복됩니다.
    - 보유 골드 표시
    ![image](https://github.com/alstn1234/TextGame/assets/102529677/914c9363-1e66-4c4f-9ab0-ab7b3d3fcc2a)

    
- 5 - 2. 레벨업 기능  (난이도 - ★★☆☆☆) - 선행 과제 [9. 던전입장]
    - 던전을 여러번 클리어할 수록 레벨이 증가합니다.
    - 레벨업시 기본 공격력이 1 방어력이 1 증가합니다.
   ![image](https://github.com/alstn1234/TextGame/assets/102529677/a278b64e-4cc6-4203-905b-eda5cf3d4c58)

      
- 6. 게임 저장하기 (난이도 - ★★★★★★)
    -  앱을 껏다 켜도 사용되던 정보를 유지합니다.
  ![image](https://github.com/alstn1234/TextGame/assets/102529677/32d49595-6b6a-4ff8-9d89-958c493a4c1d)

