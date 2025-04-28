# 1stUnityProject
레전드 팀의 Unity Game Project

<게임 후보>
1. 닷지 (탄막 피하기)
2. 뱀서 (탑뷰 슈팅 - 몬스터 피하기)
3. 아이작(탑뷰 슈팅 - 아이템 조합 위주)
4. 인피니트 런(무한루프) 위에 영상 비슷한?
5. 굶지마(로그라이크 생존)
6. 폴가이즈

가제. Project.fall

1. 아이디어 구상
  - 장르 : 타임어택 레퍼런스 게임 (폴가이즈)

2. 게임 시스템
1) 플레이어
  - 이동 : wasd - 
  - 잡기 : 마우스클릭(0) 
  - 점프 : space 
  - 점프 대쉬 : space + w
  - 시점(카메라) : 플레이어 오브젝트 뒤에 카메라를 붙여서 플레이 화면을 보여주고 마우스를 이동하여 카메라 회전

2) 맵 (prefab)
  - 타일 - 마찰계수 조절
  - 일반
  - 미끄러운 
  - 미는 벽 : 코루틴활용. 앞뒤 무빙
  - 사각 벽 : 밀면 밀리는 대로 캐릭터가 밀리는 느낌
  - 원형 기둥 : 부딫히면 튕겨내는 느낌
  - 움직이는 바닥 : 런닝머신처럼 일정한 방향으로 전진하는 타일. 마찰이 커서 캐릭터가 딸려가도록 연출.
  - 대포 : 오브젝트 풀.
  - 망치 : 코루틴 활용해서 일정하게 회전하도록, 맞으면 Addforce.Impulse로 힘을 가해 멀리 날아가도록 구현.
  - 물 : 밑에서 차올라 플레이어가 닿으면 OnTrigger와 OnCollision을 활용하여 Game Over

3) 매커니즘 
  - 게임 클리어 조건 : 결승선에 골인
  - 게임 오버 조건 : 물에 닿았을 경우. (발판 아래는 다 물이 차있으니까)
    바로 아래 사각형을 콜리더만 있게해서 천천히 올라오는 식
    물리 충돌은 x, 트리거로 플레이어 한정 게임오버 조건이 만족

4) UI UX
  - 메인 메뉴 : 기존에 있는 에셋 활용해서 배경 이미지 삽입
  - 플레이(btn) - 이벤트 연결
  - 환경설정(btn) - 이벤트 연결
  - 종료(btn) - 이벤트 연결
5) HUD 
  - 시간표시 - 남은 시간(타임어택)
  - 명예의 전당 - 시간을 표기해야하지 않을까? 
  - 인게임 랭킹 - 데이터를 저장할 수 있다면 보글보글 처럼 걸린시간 혹은 남은시간을 표기하는 식으로 랭킹을 등록(저장)

(시간이 되고, QA가 끝나고 메인에 붙였을 때) 
아바타 : 맵을 깨면 아바타 변경 시스템 해금? -> 여유가 있다면 캐릭터를 불러올 때 다른 prefab or material변경?

(아바타 시스템이 완료되고 그 이후) 
튜토리얼 : 구현해야할 것들이 많고 인게임에 구현해야할 요소가 많아서 신경 쓸 여유가 없을 것 같다.

<그래픽 스타일, 사운드 에셋>
Dimention : 3D
카메라 : 탑뷰

<플레이어>
https://assetstore.unity.com/packages/3d/characters/humanoids/ultimate-hypercasual-characters-43-bodytypes-202182#description
https://assetstore.unity.com/packages/3d/characters/humanoids/3d-characters-pro-casual-287455

<타일 & 맵 오브젝트>
https://assetstore.unity.com/packages/3d/characters/paper-man-5-city-skins-hyper-casual-characters-205577
https://assetstore.unity.com/packages/3d/environments/traps-and-obstacles-pack-195360
https://assetstore.unity.com/packages/3d/props/food/sweets-pack-low-poly-series-184660
https://assetstore.unity.com/packages/3d/props/interior/kindergarten-interior-111197
https://assetstore.unity.com/packages/3d/environments/toon-water-8143

<사운드>
https://assetstore.unity.com/packages/audio/sound-fx/hyper-casual-voice-and-sfx-pack-198783
https://assetstore.unity.com/packages/audio/sound-fx/fun-casual-sounds-64048

<이펙트>
https://assetstore.unity.com/packages/vfx/particles/fire-explosions/realistic-vfx-particles-fireworks-pack-01-264606
https://assetstore.unity.com/packages/vfx/particles/party-confetti-fx-121830

<스카이박스>
https://assetstore.unity.com/packages/2d/textures-materials/sky/beautiful-sky-pack02-253864
https://assetstore.unity.com/packages/2d/textures-materials/sky/beautiful-sky-208439
https://assetstore.unity.com/packages/2d/textures-materials/sky/cartoon-stylized-hdri-sky-pack-01-157447


<작업 우선순위>
오브젝트 기능
1. 플레이어 이동 로직, 카메라 시점 - 기운, 지혁
2. 맵 오브젝트 6종류 - 8~9가지 - 인권, 종원, 석원
(프리팹을 제작을 다하고 기능 검사까지 끝냈으면 공용폴더에 넣고 자기 폴더 테스트씬에서 활용할 수 있도록 하는 과정)

<게임 동작 로직(추후에)>
1. UI, 이벤트 - 이펙트 담당, 이미지, 사운드
2. 매니저 - 게임 오버 이벤트 관리, 데이터 저장
3. 맵 구현 

<일정 계획>
4/28 : 오브젝트 구현 
4/29 : 맵 구현
아침도 좋고 28일 저녁에 오브젝트 간 상호작용 QA
4/30 : 게임 동작 로직
5/2 : UI, 사운드, 이벤트, 매니저
5/4 : 에셋 적용
5/7 : QA 진행, 마무리 버그 픽스

<생각보다 구현속도가 빠르면 추가 구현> (구현을 아직 안해봤기 때문에 아직 모르지만)
1. 카메라 시점
2. 맵을 추가(튜토리얼)
3. 세이브 포인트 - trigger enter exit에 따라 spawnPos 변경 
4. 환경설정 사운드 조절, 밝기 조절
5. 아바타 스킨 교체

