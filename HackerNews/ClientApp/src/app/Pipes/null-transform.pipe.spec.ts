import { NullTransformPipe } from './null-transform.pipe';

describe('NullTransformPipe', () => {
  it('create an instance', () => {
    const pipe = new NullTransformPipe();
    expect(pipe).toBeTruthy();
  });
});
