import { Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import {
  ElementTypeEnum,
  FormatType,
  NgWhiteboardService,
  ToolsEnum,
  WhiteboardElement
} from 'ng-whiteboard';
import { Store } from "@ngrx/store";
import { AppState } from "../../store/app.reducer";
import {
  selectSelectedWhiteboard,
  selectSelectedWhiteboardId,
  selectWhiteboardLoading
} from "./store/whiteboard.selectors";
import { distinctUntilChanged, Observable, ReplaySubject, takeUntil, withLatestFrom } from "rxjs";
import { filter, map, take } from "rxjs/operators";
import { getWhiteboard, whiteboardUpdated } from "./store/whiteboard.actions";
import { selectCurrentGroupId } from "../../groups/group-hub/store/group-hub.selectors";
import { ActivatedRoute } from "@angular/router";
import { ExtendedWhiteboardElement, Whiteboard } from "./store/whiteboard.reducer";

@Component({
  selector: 'app-whiteboard',
  templateUrl: './whiteboard.component.html',
  styleUrls: ['./whiteboard.component.scss'],
  providers: [NgWhiteboardService],
  encapsulation: ViewEncapsulation.ShadowDom,
})
export class WhiteboardComponent implements OnInit, OnDestroy {
  @ViewChild('workarea', {static: false}) private workarea!: ElementRef<HTMLElement>;
  private destroyed$ = new ReplaySubject<boolean>(1);

  isLoading$: Observable<boolean>;
  isLoading = false;

  whiteboard$: Observable<Whiteboard>;

  toolsEnum = ToolsEnum;
  elementTypeEnum = ElementTypeEnum;
  selectedTool: ToolsEnum = ToolsEnum.BRUSH;
  selectedElement!: WhiteboardElement;
  data: WhiteboardElement[] = [];
  changes: WhiteboardElement[] = [];

  options = {
    strokeColor: '#ff0',
    strokeWidth: 5,
    fill: '#000',
    backgroundColor: '#fff',
    canvasHeight: 1000,
    canvasWidth: 1500,
    dasharray: ''
  };

  formatTypes = FormatType;
  outerWidth = 1200;
  outerHeight = 750;
  zoom = 1;
  x = 0;
  y = 0;

  isExternalChange = false;

  constructor(private _whiteboardService: NgWhiteboardService, private store: Store<AppState>, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.whiteboard$ = this.store.select(selectSelectedWhiteboard).pipe(
      takeUntil(this.destroyed$),
    );

    let isSizeCalculated = false;
    this.whiteboard$.pipe(filter(w => !!w))
      .subscribe(w => {
        if (isSizeCalculated) return;
        setTimeout(() => {
          this.calculateSize();
          isSizeCalculated = true;
        }, 0);
      });

    this.isLoading$ = this.store.select(selectWhiteboardLoading).pipe(takeUntil(this.destroyed$));

    this.isLoading$
      .subscribe(isLoading => this.isLoading = isLoading);

    this.activatedRoute.paramMap.pipe(
      takeUntil(this.destroyed$),
      map(p => +p.get('whiteboardId')),
      filter(id => !!id),
      distinctUntilChanged(),
      withLatestFrom(this.store.select(selectCurrentGroupId).pipe(filter(id => !!id)))
    ).subscribe(([whiteboardId, groupId]) => {
      this.store.dispatch(getWhiteboard({ groupId, id: whiteboardId }));
    })

    this.store.select(selectSelectedWhiteboard)
      .pipe(
        takeUntil(this.destroyed$),
        distinctUntilChanged(),
        filter(w => !!w)
      ).subscribe(whiteboard => {
      this.data = [...whiteboard.whiteboardElements.map(e => ({...e}))];
      console.log('selectSelectedWhiteboard data', this.data)
    });
  }

  calculateSize() {
    console.log('Calculate size');
    const workarea = this.workarea.nativeElement;
    const dim = {
      w: this.options.canvasWidth,
      h: this.options.canvasHeight,
    };
    let w = workarea.clientWidth;
    let h = workarea.clientHeight;
    const w_orig = w,
      h_orig = h;
    const zoom = this.zoom;

    const multi = 2;
    w = Math.max(w_orig, dim.w * zoom * multi);
    h = Math.max(h_orig, dim.h * zoom * multi);
    const scroll_x = w / 2 - w_orig / 2;
    const scroll_y = h / 2 - h_orig / 2;

    this.outerWidth = w;
    this.outerHeight = h;
    this.updateSize(dim.w, dim.h);

    setTimeout(() => {
      workarea.scrollLeft = scroll_x;
      workarea.scrollTop = scroll_y;
    }, 0);
  }

  updateSize(w: number, h: number) {
    this.options.canvasWidth = w;
    this.options.canvasHeight = h;
    const current_zoom = this.zoom;
    const contentW = this.outerWidth;
    const contentH = this.outerHeight;
    const x = contentW / 2 - (w * current_zoom) / 2;
    const y = contentH / 2 - (h * current_zoom) / 2;
    setTimeout(() => {
      this.x = x;
      this.y = y;
    }, 0);
  }

  zoomWheel(e: Event) {
    const ev = e as WheelEvent;

    if (ev.altKey || ev.ctrlKey) {
      e.preventDefault();
      const zoom = this.zoom * 100;
      this.setZoom(Math.trunc(zoom - (ev.deltaY / 100) * (ev.altKey ? 10 : 5)));
    }
  }

  setZoom(new_zoom: string | number) {
    const old_zoom = this.zoom;
    let zoomlevel = +new_zoom / 100;
    if (zoomlevel < 0.001) {
      zoomlevel = 0.1;
    }
    const dim = {
      w: this.options.canvasWidth,
      h: this.options.canvasHeight,
    };
    let animatedZoom = null;
    if (animatedZoom != null) {
      window.cancelAnimationFrame(animatedZoom);
    }
    // zoom duration 500ms
    const start = Date.now();
    const duration = 500;
    const diff = zoomlevel - old_zoom;
    const animateZoom = () => {
      const progress = Date.now() - start;
      let tick = progress / duration;
      tick = Math.pow(tick - 1, 3) + 1;
      this.zoom = old_zoom + diff * tick;
      this.updateSize(dim.w, dim.h);

      if (tick < 1 && tick > -0.9) {
        animatedZoom = requestAnimationFrame(animateZoom);
      } else {
        this.zoom = zoomlevel;
        this.updateSize(dim.w, dim.h);
      }
    };
    animateZoom();
  }

  setSizeResolution(value: string) {
    let w = this.options.canvasWidth;
    let h = this.options.canvasHeight;
    console.log(value);
    const dims: number[] = [];
    dims[0] = parseInt(value.split('x')[0]);
    dims[1] = parseInt(value.split('x')[1]);
    if (value == 'Custom') {
      return;
    } else if (value == 'content') {
      dims[0] = 100;
      dims[1] = 100;
    }
    const diff_w = dims[0] - w;
    const diff_h = dims[1] - h;

    let animatedSize = null;
    if (animatedSize != null) {
      window.cancelAnimationFrame(animatedSize);
    }
    const start = Date.now();
    const duration = 500;

    const animateCanvasSize = () => {
      const progress = Date.now() - start;
      let tick = progress / duration;
      tick = Math.pow(tick - 1, 3) + 1;
      w = parseInt((dims[0] - diff_w + tick * diff_w).toFixed(0));
      h = parseInt((dims[1] - diff_h + tick * diff_h).toFixed(0));
      this.updateSize(w, h);
      if (tick < 1 && tick > -0.9) {
        animatedSize = requestAnimationFrame(animateCanvasSize);
      } else {
        this.updateSize(w, h);
      }
    };
    animateCanvasSize();
  }

  onDragDown(input: HTMLInputElement, selectedElement: any, prop: string | number) {
    const min = input.min ? parseInt(input.min, 10) : null;
    const max = input.max ? parseInt(input.max, 10) : null;
    const step = parseInt(input.step, 10);
    let area = 200;
    if (min && max) {
      area = max - min > 0 ? (max - min) / step : 200;
    }
    const scale = (area / 70) * step;
    let lastY = 0;
    let value = parseInt(input.value, 10);

    const onMouseMove = (e: MouseEvent) => {
      if (lastY === 0) {
        lastY = e.pageY;
      }
      const deltaY = (e.pageY - lastY) * -1;
      lastY = e.pageY;
      let val = deltaY * scale * 1;
      const fixed = step < 1 ? 1 : 0;
      val.toFixed(fixed);
      val = Math.floor(Number(value) + Number(val));

      if (max !== null) val = Math.min(val, max);
      if (min !== null) val = Math.max(val, min);
      value = val;

      selectedElement[prop] = value;
      input.value = value.toString();
    };
    const onMouseUp = () => {
      document.removeEventListener('mousemove', onMouseMove);
      document.removeEventListener('mouseup', onMouseUp);
    };
    document.addEventListener('mousemove', onMouseMove);
    document.addEventListener('mouseup', onMouseUp);
  }

  setNumberValue(obj: any, prop: string, value: number): void {
    if (!isNaN(value)) {
      obj[prop] = value;
    }
  }

  toggleFontWeight() {
    if (this.selectedElement.options.fontWeight === 'normal') {
      this.selectedElement.options.fontWeight = 'bold';
    } else {
      this.selectedElement.options.fontWeight = 'normal';
    }
  }

  toggleFontStyle() {
    if (this.selectedElement.options.fontStyle === 'normal') {
      this.selectedElement.options.fontStyle = 'italic';
    } else {
      this.selectedElement.options.fontStyle = 'normal';
    }
  }

  newDocument() {
    this._whiteboardService.erase();
  }

  saveAs(format: FormatType) {
    this._whiteboardService.save(format);
  }

  addImage(fileInput: EventTarget | null) {
    if (fileInput) {
      const files = (fileInput as HTMLInputElement).files;
      if (files) {
        const reader = new FileReader();
        reader.onload = (e: ProgressEvent) => {
          const image = (e.target as FileReader).result;
          this._whiteboardService.addImage(image as string);
        };
        reader.readAsDataURL(files[0]);
      }
    }
  }

  undo() {
    this._whiteboardService.undo();
  }

  redo() {
    this._whiteboardService.redo();
  }

  colorChange(propName: 'fill' | 'strokeColor', color: any) {
    if (this.selectedElement) {
      this.selectedElement.options[propName] = color;
    } else {
      this.options[propName] = color;
      this.updateOptions();
    }
  }

  swapColors() {
    [this.options.fill, this.options.strokeColor] = [this.options.strokeColor, this.options.fill];
    this.updateOptions();
  }

  updateOptions() {
    this.options = Object.assign({}, this.options);
  }

  onDataChange(data: WhiteboardElement[]) {
    const change = data.filter(e => !e.id.endsWith('_External') && !this.changes.some(c => c === e));

    console.log('Change', change);
    this.data = data;

    if (!change?.length) {
      return;
    }

    this.changes = [...this.changes, ...change];

    this.store.select(selectCurrentGroupId)
      .pipe(
        takeUntil(this.destroyed$),
        take(1),
        withLatestFrom(this.store.select(selectSelectedWhiteboardId).pipe(filter(id => !!id)))
      ).subscribe(([groupId, selectedWhiteboardId]) => {
      this.store.dispatch(whiteboardUpdated({groupId, id: selectedWhiteboardId, changes: change.map(e => e as ExtendedWhiteboardElement)}));
    });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
  }
}
