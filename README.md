<div align="center">

<img src="https://capsule-render.vercel.app/api?type=waving&color=FFFFBF&height=275&section=header&text=Antocalypse&fontSize=72&fontAlign=50&fontAlignY=40&desc=UNITY&descSize=18&descAlign=79&descAlignY=51" />

</div>

# 1. Unity Project Convention
- Code Style: Allman
- Ref: [유니티 프로젝트를 구성하기 위한 방법](https://velog.io/@jaehyeoksong0/unity-organizing-your-project)



# 2. Git Hub Commit Convention
- Ref: [Git Commit Message Convention](https://github.com/gyoogle/tech-interview-for-developer/blob/master/ETC/Git%20Commit%20Message%20Convention.md)
### 커밋 메세지 형식

```
type: Subject (제목)

body (본문)

footer (꼬리말)
```

- ```feat``` : 새로운 기능에 대한 커밋
- ```fix``` : 버그 수정에 대한 커밋
- ```build``` : 빌드 관련 파일 수정에 대한 커밋
- ```chore``` : 그 외 자잘한 수정에 대한 커밋
- ```ci``` : CI관련 설정 수정에 대한 커밋
- ```docs``` : 문서 수정에 대한 커밋
- ```style``` : 코드 스타일 혹은 포맷 등에 관한 커밋
- ```refactor``` :  코드 리팩토링에 대한 커밋
- ```test``` : 테스트 코드 수정에 대한 커밋

### Subject (제목)
- *한글*로 간결하게 작성

### Body (본문)
- 상세히 작성, 기본적으로 무엇을 왜 진행 하였는지 작성

### footer (꼬리말)
- 참고사항

  

# 3. Branch Rule
- Ref: [Git Branch & Naming](https://ej-developer.tistory.com/75)
### 크게 3가지 유형의 브랜치로 분기하여 사용

- ```main``` : 유저에게 배포가능한 상태를 관리하는 브랜치. 절대 함부로 병합 시키지 말것
- ```develop``` : 기능개발을 위한 브랜들을 병합시키는 브랜치. feature/... 브랜치는 이곳에서 분기하여 병합, 안정적인 상태일때, main에 병합
- ```feature/...``` : 새로운 기능 및 버그 수정이 필요할 때 사용하는 브랜치. develop 브랜치에서 분기하여 병합, 더 이상 필요가 없다면 삭제 naming ex) feature/dialogue
  
<div align="center">
<img src="https://capsule-render.vercel.app/api?type=waving&color=FFFFBF&height=150&section=footer" />
</div>
