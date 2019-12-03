# BCI_Hololens_to_things

### 새로운 버전 빌드 시

  1. Unity project 파일 다른 이름으로 저장
  1. Unity build Settings 에서 Player Settings 선택
  1. Product Name, Package name 수정
  1. Build
  1. sln 파일에서 Debug -> Master, ARM -> x86
  1. package.appxmanifest 파일에서 패키징, 인증서 선택
  1. 인증서 구성 -> 테스트 인증서 만들기
  1. Universal windows 파일 -> 스토어 -> 앱 패키지 만들기
  1. 사이드 로드할 패키지 만들기
  1. x64, ARM 말고 x86만 생성
  1. 빌드 완료 되면 홀로렌즈에 
