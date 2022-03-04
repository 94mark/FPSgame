# 1인칭 슈팅 3D FPS 게임 데모 제작
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
- 카운터 스트라이크 온라인(csonline)과 같은 1인칭 슈팅/폭파 게임 기획 
### 2.2 개발 과정
 - 프로토타입 버전 : 기본적인 기능 구현
 - 알파 타입 버전 : UI, 그래픽 퀄리티 개선 , 애셋 다각화, 레벨 디자인 및 포스트프로세싱/사운드/이펙트 효과 추가
## 3. 핵심 구현 내용 
### 3.1 Camera 1인칭 시점
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
	+ 공격 애니메이션 타임라인의 특정 시점에 이벤트 키를 추가해 Player 피격 시 HitEvent(데미지 발생 및 피격 UI 활성화)가 발생하도록 구현
- 길 찾기(path finding) 알고리즘 구현 
	+ Enemy의 이동 방식을 NavMesh로 이동하도록 변경
	+ OffMeshLink를 적용한 Navigation Mesh를 Bake하여 장애물을 넘어 최단 거리를 이동하는 알고리즘 구현
	
## 4. 문제 해결 내용
### 4.1 Enemy 상태 전환 시 미끄러지듯 이동하는 문제
- Enemy가 NavMesh로 이동하여 Player에 접근하면 공격 상태로 전환
- NavMeshAgent는 매 프레임마다 목적지의 위치를 체크하며 이동하기 때문에 Player가 갑자기 이동하면 Enemy가 공격 동작을 하면서 미끄러지듯 이동하는 원인 파악 
- ResetPath() 함수를 사용하여 NavMeshAgent의 이동을 멈추고 목적지 경로를 초기화, 예외 처리
