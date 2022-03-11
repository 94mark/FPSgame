# 1인칭 슈팅 3D FPS 게임 데모 제작
![image](https://user-images.githubusercontent.com/90877724/157864897-52178650-07ac-4e6b-ab39-6a328bfa0cde.png)
![image2](https://user-images.githubusercontent.com/90877724/157864908-e0527e30-0df9-47ec-9925-e84bfcaca781.png)
## 1. 프로젝트 개요
### 1.1 개발 인원/기간 및 포지션
- 1인, 총 5일 소요
- 프로그래밍
### 1.2 개발 환경
- unity 2020.3.16f
- 언어 : C#
- OS : Window 10			
## 2. 개발 단계
### 2.1 벤치마킹
- <카운터 스트라이크 온라인>과 같은 1인칭 슈팅/폭파 게임 기획 
### 2.2 개발 과정
 - 프로토타입 버전 : 기본적인 기능 구현
 - 알파 타입 버전 : UI, 그래픽 퀄리티 개선 , 애셋 다각화, 레벨 디자인 및 포스트프로세싱/사운드/이펙트 효과 추가
## 3. 핵심 구현 내용 
### 3.1 카메라 1인칭 시점
- transform.eulerAngles에 방향 벡터 값을 누적시켜 rotate
- Mathf.Clamp() 함수를 사용해 x축 회전(상하회전) 값을 -90 ~ 90도 사이로 제한(사람의 시선 모사)
- TransfromDirection() 함수를 사용해 Main Camera를 기준으로 방향 벡터 변환
### 3.2 무기 제작
- 수류탄 Bomb 투척/폭발 효과 및 데미지 기능 구현
	+ AddForce() 함수를 사용해 카메라의 정면 방향으로 수류탄 투척, ForceMode를 Impulse로 선언하여 순간적인 힘을 가하고 질량에 영향을 받지 않도록 함
	+ 폭발 범위 내에서 모든 객체의 충돌 처리를 위해 Physics.OverlapSphere()함수 사용
	+ 폭발 효과 반경 내에서 Layer가 'Enemy'인 모든 객체의 Collider 컴포넌트를 Collider[] 배열에 저장
	+ for문을 사용해 저장된 Collider 배열에 있는 모든 Enemy에게 bombEffect 적용
- 총 계열 무기 발사/피격 효과/데미지 및 스나이퍼 모드 구현
	+ Physics.Raycast()를 사용해 Ray발사, RaycastHit에 저장된 위치정보에 피격 이벤트 플레이
	+ 피격 이펙트의 forward 방향을 ray가 부딪힌 지점의 법선 벡터(hitInfo.normal)와 일치시켜 이펙트 실행 결과 개선 
	+ 코루틴 함수를 사용해 총구 이펙트 오브젝트가 랜덤하게 실행되도록 사격 이펙트 효과 구현 
	+ 일반 모드는 Camera.main.fieldOfView를 15f로, 스나이퍼 줌 모드는 60f로 설정, KeyCode입력 값을 통해 스나이퍼 인/아웃 모드 활성화
### 3.3 에너미 FSM 제작
- FSM 상태 구조 설계 (대기 - 이동 - 공격/공격대기 - 피격 - 죽음)
	+ enum EnemysState로 상태 상수 선언, switch case 문으로 상태별 정해진 기능 및 애니메이션을 수행하도록 코드 구현
	+ 피격 행동 절차를 처리하는 DamageProcess() 와 죽음 상태를 처리하는 DieProcess() 함수는 코루틴으로 처리해 성능 개선
	+ 공격 애니메이션 타임라인의 특정 시점에 이벤트 키를 추가해 Player 피격 시 HitEvent(데미지 발생 및 피격 UI 코루틴 활성화)가 발생하도록 구현
- 길 찾기(path finding) 알고리즘 구현 
	+ Enemy의 이동 방식을 NavMesh로 이동하도록 변경
	+ OffMeshLink를 적용한 Navigation Mesh를 Bake하여 장애물을 넘어 최단 거리를 이동하는 알고리즘 구현
### 3.4 로그인 화면/비동기 씬 로드
- Login Scene 구현
	+ PlayerPref.SetString() 함수를 사용하여 id (key) / password (value) 데이터 저장
	+ PlayerPref.HasKey() 함수를 사용하여 중복 아이디 생성 방지
	+ PlayerPref.GetString()을 통해 사용자가 입력한 아이디를 키로 사용해 시스템에 저장된 값을 불러오고 입력한 패스워드와 비교해 동일하면 LoadScene(i) 호출
- 비동기 씬 로딩 
	+ AsyncOperation 비동기 씬 로드 코루틴 사용
	+ allowSceneActivation = false로 선언해 씬 로딩이 완료되기 전 까지 화면 출력 제한
	+ progress 변수를 선언해 로딩 진행률을 슬라이더 바와 텍스트로 표시
	+ Restart 게임 시 비동기 씬이 로드되도록 추가
### 3.5 GameManager 관리
- 게임 매니저 클래스를 싱글턴 패턴으로 구현
- '게임 중' 상태가 아닐 시, 플레이어 이동/회전/무기 발사 및 카메라 회전 조작 제한
### 3.6 그래픽 개선
- FX_Fire(불꽃) / FX_Smoke(연기) 파티클 오브젝트를 커스터마이징하여 레벨 디자인 향상 
- 라이트 맵핑(Light Mapping)을 통해 성능 개선, Directional Light를 Mixed로 설정하여 Generate Lighting
- Light Probe를 세팅하여 Player 중심의 조명 효과 구현
- 포스트프로세싱 활용, DOF(Depth Of Field)의 Apeture 값을 조절해 카메라 효과 개선
- Cinemachine의 버추얼 카메라를 사용해 게임플레이 영상 레코딩
## 4. 문제 해결 내용
### 4.1 Player 대기 애니메이션 실행 시 게임 뷰에서 무기 오브젝트가 뚫려 보이는 듯한 문제
- 메인 카메라의 촬영 범위에 따른 문제 발생 (참조 - 절두체(View frustrum) : 카메라에 찍히는 공간 영역의 범위를 시각적으로 표현) 
- 카메라 위치로부터 절두체 시작점까지의 거리인 Clipping Planes.Near 값을 조정
- 추가로 Far 값을 조정하여 출력해야 하는 객체 연산량을 줄이고 게임 성능을 향상
### 4.2 Enemy 상태 전환 시 미끄러지듯 이동하는 문제
- Enemy가 NavMesh로 이동하여 Player에 접근하면 공격 상태로 전환
- NavMeshAgent는 매 프레임마다 목적지의 위치를 체크하며 이동하기 때문에 Player가 갑자기 이동하면 Enemy가 공격 동작을 하면서 미끄러지듯 이동하는 원인 파악 
- ResetPath() 함수를 사용하여 NavMeshAgent의 이동을 멈추고 목적지 경로를 초기화, 예외 처리
