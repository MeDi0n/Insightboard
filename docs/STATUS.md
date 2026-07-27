# Где мы стоим (для продолжения работы)

Этот файл — точка входа в проект после перерыва или на новой машине.
Читать вместе с `CLAUDE.md` (как со мной работать) и `PROJECT.md` (что строим).

## Готово

**Вся серверная логика собрана и работает end-to-end** (фейковый AI вместо настоящего):

```
CSV → CsvParser (реальные колонки из заголовка)
    → PromptBuilder (промпт из колонок + ошибки в ретрай)
    → IAiProvider (козырь №1: провайдер за интерфейсом, через DI)
    → Validator (козырь №2: 3 слоя — синтаксис/структура/семантика, fail-early)
    → петля ретрая до 3× с обратной связью по ошибкам
    → done/failed → DashboardStore → фронт
```

- **Backend** (`backend/`): контроллер тонкий, оркестратор `DashboardGenerator` держит петлю, все роли внедрены через DI (`Program.cs`). Оба козыря на месте.
- **Frontend** (`frontend/`): загрузка CSV → 202+id → поллинг `GET /{id}` через TanStack Query → рендер. **Графики пока рендерятся ТЕКСТОМ**, не диаграммами.
- Ретрай проверен вживую (ломали фейк `bar`→`banana` → 3 неудачи → `failed`).

## Следующий шаг — две большие цели (выбрать одну)

1. **Настоящий AI.** Заменить `FakeAiProvider` на реальный (OpenAI/Anthropic) за тем же `IAiProvider` — одна строка в `Program.cs`. Ключ хранить ТОЛЬКО в user-secrets (dev) / env (prod), НИКОГДА в коде/git. Тогда оживёт умный ретрай.
2. **Фронт: библиотека графиков.** Заменить текстовый рендер в `DashboardView` на реальные диаграммы (Recharts/Chart.js).

## Долги (не потерять)

1. `NU1903` — обновить Microsoft.OpenApi.
2. Варнинг `Failed to determine https port` — почистить `UseHttpsRedirection` для локалки.
3. `frontend/.oxlintrc.json` — удалить огрызок.
4. Хардкод-URL `http://localhost:5029` во фронте — вынести в env/конфиг.
5. Пример JSON в промпте (`PromptBuilder`) — черновой, отполировать под реальный AI.

## Как запускать

- Backend: из `backend/` → `dotnet watch run` (слушает `http://localhost:5029`).
- Frontend: из `frontend/` → `npm install` (первый раз) → `npm run dev` (`http://localhost:5173`).
- Тестовый CSV: колонки `month,sales,awards`.
