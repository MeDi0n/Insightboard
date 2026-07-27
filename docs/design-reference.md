# Insightboard — дизайн-эталон

Целевой визуальный стиль фронтенда. Стиль сгенерён в дизайн-инструменте, здесь зафиксированы
**дизайн-токены** и **описания всех экранов** для переноса в реальные компоненты.

---

## ⚠️ ВАЖНО для ассистента (режим работы)

**Не писать код за меня.** Я — джуниор, делаю проект, чтобы научиться и защитить на собесе.
При внедрении стилей:
1. Разложи задачу на шаги (какой компонент, какие токены, в каком порядке).
2. Объясни концепцию (что за CSS-приём, зачем, какие есть варианты).
3. Дай мне писать самому, потом отревьюй.
Токены и описания ниже — это **эталон и справка**, а НЕ готовый код для копипаста в компоненты.
Подробнее — в `CLAUDE.md`. Где стоит проект — в `docs/STATUS.md`.

---

## Система (кратко)

Светлая аналитическая SaaS в духе Linear / Notion / Stripe: белый холст, много воздуха,
карточки с мягкой тенью и тонким бордером, точечный индигово-синий акцент (`#4f46e5`).
Тёмная тема — те же токены и иерархия на глубоком нейтральном фоне (`#0e1116`), акцент светлее
(`#818cf8`). Переключение через `data-theme="dark"` на корне. Типографика — Inter, сетка отступов на 4px.

Компоненты, на которые ложится дизайн (реальная структура проекта):
- Шапка `Insightboard` + иконка-лого + переключатель темы (луна).
- `UploadForm` — экран загрузки (dropzone + кнопки).
- `DashboardView` — состояния: загрузка/generating (processing), дашборд (done), ошибка (failed).
- `Charts/Chart` → `BarChartView` / `LineChartView` (Recharts), в карточках.

---

## Экраны и состояния (по скриншотам эталона)

### 1. Загрузка (начальный экран)
- **Шапка:** слева лого — иконка столбиков (индиго) + «Insightboard» (тёмный, semibold). По центру —
  навигация-табы; активный таб = индиго-заливка `--color-accent-subtle` со скруглением. Справа — кнопка
  переключения темы (иконка луны) в скруглённом квадрате с бордером.
- **Контент (по центру, в белой карточке-холсте с мягкой тенью):**
  - Заголовок «Постройте дашборд из CSV» — крупный (`--font-size-2xl`), semibold, `--color-text-primary`.
  - Подзаголовок — `--color-text-secondary`: «Загрузите таблицу — ИИ подберёт подходящие графики и соберёт дашборд автоматически.»
  - **Dropzone:** большая зона с **пунктирным** бордером (`--color-border`), скругление `--radius-card`,
    фон чуть светлее. Внутри по центру: иконка загрузки (индиго), текст «Перетащите CSV файл сюда»,
    строчка «или» (muted), белая кнопка `Выбрать файл` (бордер, скругление `--radius-control`).
  - Ниже — **основная кнопка** «Построить дашборд»: заливка `--color-accent`, белый текст, `--radius-control`.

### 2. Генерация (processing)
- Та же шапка.
- По центру: **пилюля-бейдж** «Генерируем дашборд…» с точкой, фон `--color-accent-subtle`, текст акцентный.
- Ниже — подпись `--color-text-secondary`: «ИИ анализирует структуру данных и подбирает типы графиков».
- **Скелетоны:** 2 крупные карточки-заглушки будущих графиков — светло-серые плейсхолдеры с мягким
  градиентом (шиммер): полоска под заголовок + большая пустая область под график.

### 3. Дашборд (done)
- Та же шапка.
- **Сетка карточек-графиков** (на десктопе 2 в ряд, на мобиле — в столбик). Каждая карточка:
  фон `--color-bg-elevated`, бордер `--color-border`, скругление `--radius-card`, тень `--shadow-sm`.
  - Заголовок графика (semibold, `--color-text-primary`) + справа маленький **бейдж типа** («bar»/«line»)
    — скруглённая пилюля, приглушённый фон/текст.
  - **График 1 «Sales by month» — BAR:** индиго-столбцы со скруглённым верхом, ось Y 0–200,
    данные Jan 100 / Feb 150 / Mar 120 / Apr 200. Сетка светлая пунктиром, только горизонтальная.
  - **График 2 «Awards by month» — LINE:** индиго-линия с точками + лёгкая индиго-заливка под ней,
    ось Y 0–6, данные Jan 2 / Feb 3 / Mar 5 / Apr 4.

### 4. Ошибка (failed)
- Та же шапка.
- По центру: **иконка-предупреждение** (красный треугольник) в светло-красном круге (`--color-danger-subtle`).
- Заголовок «Не удалось построить дашборд» (semibold, `--color-text-primary`).
- Текст `--color-text-secondary`: «ИИ не смог собрать корректный результат после 3 попыток. Проверьте
  формат файла и попробуйте снова.»
- Основная кнопка «Загрузить другой файл» (заливка `--color-accent`).

> Скриншоты всех четырёх экранов (светлая тема) есть в переписке; при желании можно сложить их в
> `docs/design/` картинками — для визуальной сверки.

---

## Дизайн-токены (CSS-переменные)

Переносить в глобальный CSS (`frontend/src/index.css`) как есть. Тёмная тема — через `[data-theme="dark"]`
на корне (radius / spacing / шрифтовая шкала от `:root` не меняются).

```css
/* ===== Insightboard design tokens — LIGHT ===== */
:root {
  --color-bg: #f7f8fa;
  --color-bg-elevated: #ffffff;
  --color-border: #e5e7eb;
  --color-text-primary: #0f172a;
  --color-text-secondary: #475569;
  --color-text-muted: #94a3b8;
  --color-accent: #4f46e5;
  --color-accent-hover: #4338ca;
  --color-accent-subtle: #eef2ff;
  --color-danger: #dc2626;
  --color-danger-subtle: #fef2f2;

  --shadow-sm: 0 1px 2px rgba(15,23,42,0.06);
  --shadow-md: 0 4px 16px rgba(15,23,42,0.08);

  --radius-card: 12px;
  --radius-control: 8px;

  --space-1: 4px;  --space-2: 8px;  --space-3: 12px; --space-4: 16px;
  --space-5: 24px; --space-6: 32px; --space-7: 48px; --space-8: 64px;

  --font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
  --font-size-xs: 12px; --font-size-sm: 13px; --font-size-base: 14px;
  --font-size-md: 16px; --font-size-lg: 20px; --font-size-xl: 24px; --font-size-2xl: 32px;
  --font-weight-regular: 400; --font-weight-medium: 500; --font-weight-semibold: 600;

  /* Recharts */
  --chart-bar-fill: #4f46e5;
  --chart-bar-fill-hover: #4338ca;
  --chart-line-stroke: #4f46e5;
  --chart-line-fill: rgba(79,70,229,0.12);
  --chart-axis-text: #64748b;
  --chart-axis-line: #cbd5e1;
  --chart-grid: #e5e7eb;          /* CartesianGrid stroke, strokeDasharray "3 3" */
  --chart-tooltip-bg: #ffffff;
  --chart-tooltip-border: #e5e7eb;
  --chart-tooltip-shadow: 0 4px 16px rgba(15,23,42,0.12);
  --chart-tooltip-radius: 8px;
  --chart-tooltip-font-size: 13px;
}

/* ===== Insightboard design tokens — DARK ===== */
[data-theme="dark"] {
  --color-bg: #0e1116;
  --color-bg-elevated: #171a21;
  --color-border: #262b33;
  --color-text-primary: #e5e7eb;
  --color-text-secondary: #9aa4b2;
  --color-text-muted: #62697a;
  --color-accent: #818cf8;
  --color-accent-hover: #a5b4fc;
  --color-accent-subtle: rgba(129,140,248,0.16);
  --color-danger: #f87171;
  --color-danger-subtle: rgba(248,113,113,0.12);

  --shadow-sm: 0 1px 2px rgba(0,0,0,0.3);
  --shadow-md: 0 8px 24px rgba(0,0,0,0.45);

  /* radius / spacing / type scale unchanged from :root */

  /* Recharts */
  --chart-bar-fill: #818cf8;
  --chart-bar-fill-hover: #a5b4fc;
  --chart-line-stroke: #818cf8;
  --chart-line-fill: rgba(129,140,248,0.18);
  --chart-axis-text: #9aa4b2;
  --chart-axis-line: #3a3f4a;
  --chart-grid: #232830;
  --chart-tooltip-bg: #1c2029;
  --chart-tooltip-border: #2c313b;
  --chart-tooltip-shadow: 0 8px 24px rgba(0,0,0,0.5);
  --chart-tooltip-radius: 8px;
  --chart-tooltip-font-size: 13px;
}
```

---

## Как токены ложатся на Recharts (справка, реальные имена пропсов)

- **Bar:** `dataKey="sales"`, `fill={--chart-bar-fill}`, скругление верхних углов ~4px (`radius`).
- **CartesianGrid:** `stroke={--chart-grid}`, `strokeDasharray="3 3"`, `vertical={false}`.
- **XAxis / YAxis:** `stroke={--chart-axis-line}`, подписи (`tick`) `fill={--chart-axis-text}`,
  `fontSize` 12 / 11.
- **Tooltip** `contentStyle`: `background={--chart-tooltip-bg}`, `border: 1px solid {--chart-tooltip-border}`,
  `borderRadius={--chart-tooltip-radius}`, `boxShadow={--chart-tooltip-shadow}`, `fontSize={--chart-tooltip-font-size}`.
- **Line:** `dataKey="awards"`, `stroke={--chart-line-stroke}`, `strokeWidth={2.5}`, точки радиусом ~4px.

> Нюанс: цвет графика Recharts берёт из JS-пропса (`fill`/`stroke`), а не из CSS-класса. Значит
> значение переменной надо будет прочитать в JS (например через `getComputedStyle`) или задать явно —
> разберём при внедрении.

---

## План внедрения (по шагам, вместе)

1. **Фундамент:** подключить Inter, положить токены в `index.css`, задать базовые `body` (фон/текст/шрифт),
   сделать переключатель темы (`data-theme` на `<html>`).
2. **Шапка** (лого + табы/навигация + тумблер темы).
3. **UploadForm** (dropzone + кнопки).
4. **Карточка графика** + сетка дашборда (адаптив: десктоп — 2 в ряд, мобайл — столбик).
5. **Стилизация Recharts** (bar/line) через токены.
6. **Состояния** processing (скелетоны) и failed (ошибка).
